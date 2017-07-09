import argparse
import math
import time
import socket
import os.path
import sys
import os
import threading
import queue
import multiprocessing
import numpy as np
import mne
from datetime import datetime
from pythonosc import dispatcher as dp
from pythonosc import osc_server
from pythonosc import osc_message_builder
from pythonosc import udp_client

IMAGE_DIR = "images"
EXTEN = ".png"
DEFAULT_IP = "0.0.0.0"
REFRESH_RATE = 0.1
NUM_RECORDINGS = 10
THRESHOLD = 0.9
RAW_COLUMNS = ['RAW_TP9', 'RAW_AF7', 'RAW_AF8', 'RAW_TP10']
SAMPLING_RATE= 256


class EEGData():

    def __init__(self):
        self.lock = multiprocessing.Lock()
        self.num_req = 0
        self.currdata = [] 
        self.info = mne.create_info(RAW_COLUMNS, SAMPLING_RATE, ch_types = 'eeg')
        self.filtered_eeg = [0, 0, 0, 0]

    def add_data(self, new_data):

        if self.currdata == []:
            for i in range(len(new_data)):
                self.currdata.append([new_data[i]])
        else:
            for i in range(len(new_data)):
                self.currdata[i].append(new_data[i])

        self.num_req += 1
        print(self.num_req)

    def get_filtered_eeg(self):

        if self.num_req == NUM_RECORDINGS:
            self.currdata = np.array(self.currdata)
            print(self.currdata.shape)
            custom_raw = mne.io.RawArray(self.currdata, self.info)
            custom_raw = custom_raw.filter(1., 30., h_trans_bandwidth='auto', filter_length='auto', phase='zero', picks = [0,1,2,3])
            data, _ = np.array(custom_raw[:])
            data = [np.mean(x) for x in data]

        else:
            data = self.filtered_eeg
        print(str(data))
        return data



def setup_server(data):
    parser = argparse.ArgumentParser()
    ip = socket.gethostbyname(socket.gethostname())
    parser.add_argument("--ip",
                        default=ip,
                        help="The ip to listen on")
    parser.add_argument("--port",
                        type=int,
                        default=5000,
                        help="The port to listen on")
    args = parser.parse_args()

    dispatcher = dp.Dispatcher()
    dispatcher.map("/debug", print)
    dispatcher.map("/muse/eeg", lambda addr, args, ch1, ch2, ch3, ch4, ch5: eeg_handler(addr, args, ch1, ch2, ch3, ch4, ch5, data), "EEG")

    server = osc_server.ThreadingOSCUDPServer(
        (args.ip, args.port), dispatcher)
    server.socket.setblocking(0)
    print("Serving on {}".format(server.server_address))
    return server

def setup_client(data):
    ip = socket.gethostbyname(socket.gethostname())
    parser = argparse.ArgumentParser()
    parser.add_argument("--ip", default= ip, help="The ip of the OSC server")
    parser.add_argument("--port", type=int, default=5000, help="The port the OSC server is listening on")
    args = parser.parse_args()

    client = udp_client.SimpleUDPClient(args.ip, args.port)
    print("Serving on {}".format(client._address))
    print("Serving on {}".format(client._port))
    return client

def eeg_handler(unused_addr, args, ch1, ch2, ch3, ch4, ch5, eeg):

    data = [ch1, ch2, ch3, ch4]
    currmax = np.nan_to_num(data)
    eeg.add_data(currmax)


def start_server(data):
    t = threading.Thread(target=setup_server(data))
    t.setDaemon(True)
    t.start()

if __name__ == "__main__":
    data = EEGData()
    server = setup_server(data)
    client = setup_client(data)

    count_trials = 0
    like = False
    while 1:

        num_requests = 0
        while num_requests < NUM_RECORDINGS:
            server.handle_request()
            num_requests = data.num_req
        num_requests = 0
        filtered_eeg = data.get_filtered_eeg()
        data.num_req = 0
        data.currdata = []
        client.send_message("/muse/eeg", filtered_eeg)
        client.send_message("/muse/elements/alpha_absolute", filtered_eeg)
        client.send_message("/muse/elements/beta_absolute", filtered_eeg)
        client.send_message("/muse/elements/delta_absolute", filtered_eeg)
        client.send_message("/muse/elements/theta_absolute", filtered_eeg)
        client.send_message("/muse/elements/gamma_absolute", filtered_eeg)
        