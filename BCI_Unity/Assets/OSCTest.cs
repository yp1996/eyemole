using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class OSCTest : MonoBehaviour {
	
	private Dictionary<string, ServerLog> servers;

	public static double[] eegData;
	public static double[] alphaData;
	public static double[] betaData;
	public static double[] deltaData;
	public static double[] thetaData;
	public static double[] gammaData;
	
	// Script initialization
	void Start() {	
		UnityEngine.Debug.Log("Started");
		OSCHandler.Instance.Init(); //init OSC
		servers = new Dictionary<string, ServerLog>();
		eegData = new double[4];
		alphaData = new double[4];
		betaData = new double[4];
		thetaData = new double[4];
		deltaData = new double[4];
		gammaData = new double[4];
	}

	// NOTE: The received messages at each server are updated here
    // Hence, this update depends on your application architecture
    // How many frames per second or Update() calls per frame?
	void Update() {
		
		OSCHandler.Instance.UpdateLogs();
		servers = OSCHandler.Instance.Servers;
		
	    foreach( KeyValuePair<string, ServerLog> item in servers )
		{
			// If we have received at least one packet,
			// show the last received from the log in the Debug console

			if(item.Value.log.Count > 0) 
			{
				int lastPacketIndex = item.Value.packets.Count - 1;

				if (item.Value.packets[lastPacketIndex].Address == "#bundle"){

					OSCMessage msg1 = (OSCMessage) (item.Value.packets[lastPacketIndex].Data[0]);

					String addr = msg1.Address;
					
					if (addr == "/muse/elements/alpha_absolute"){
						for (int i = 0; i <= 3; i++) {
							alphaData [i] = (double) msg1.Data[i];
						}
					}
					else if (addr == "/muse/elements/beta_absolute"){
						for (int i = 0; i <= 3; i++) {
							betaData [i] = (double) msg1.Data[i];
						}
					}
					else if (addr == "/muse/elements/theta_absolute"){
						for (int i = 0; i <= 3; i++) {
							thetaData[i] = (double) msg1.Data[i];
						}
					}
					else if (addr == "/muse/elements/delta_absolute"){
						for (int i = 0; i <= 3; i++) {
							deltaData[i] = (double) msg1.Data[i];
						}
					}
					else if (addr == "/muse/elements/gamma_absolute"){
						for (int i = 0; i <= 3; i++) {
							gammaData[i] = (double) msg1.Data[i];
						}
					}
					else if (addr == "/muse/eeg"){
						for (int i = 0; i <= 3; i++) {
							eegData[i] = (double) msg1.Data[i];
						}
					}


				}
			}
	    }
	}
}
