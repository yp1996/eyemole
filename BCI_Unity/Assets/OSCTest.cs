using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class OSCTest : MonoBehaviour {
	
	public OSC osc;

	public static float[] eegData;
	public static float[] alphaData;
	public static float[] betaData;
	public static float[] deltaData;
	public static float[] thetaData;
	public static float[] gammaData;
	public static float[] accData;
	
	// Script initialization
	void Start() {	
		UnityEngine.Debug.Log("Started");
		eegData = new float[4];
		alphaData = new float[4];
		betaData = new float[4];
		thetaData = new float[4];
		deltaData = new float[4];
		gammaData = new float[4];
		accData = new float[3];
		osc.SetAddressHandler( "/muse/eeg" , OnReceiveEEG );
		osc.SetAddressHandler( "/muse/elements/alpha_absolute" , OnReceiveAlpha);
		osc.SetAddressHandler( "/muse/elements/beta_absolute" , OnReceiveBeta);
		osc.SetAddressHandler( "/muse/elements/theta_absolute" , OnReceiveTheta);
		osc.SetAddressHandler( "/muse/elements/gamma_absolute" , OnReceiveGamma);
		osc.SetAddressHandler( "/muse/elements/delta_absolute" , OnReceiveDelta);
		osc.SetAddressHandler( "/muse/acc" , OnReceiveAcc);
	}

	// NOTE: The received messages at each server are updated here
    // Hence, this update depends on your application architecture
    // How many frames per second or Update() calls per frame?
	void Update() {

	}

	void OnReceiveEEG(OscMessage message) {
		for (int i = 0; i < 4; i++) {
			eegData[i] = message.GetFloat(i);
		}
	}

	void OnReceiveAlpha(OscMessage message) {
		for (int i = 0; i < 4; i++) {
			alphaData[i] = message.GetFloat(i);
		}
	}

    void OnReceiveBeta(OscMessage message) {
		for (int i = 0; i < 4; i++) {
			betaData[i] = message.GetFloat(i);
		}
	}

	void OnReceiveGamma(OscMessage message) {
		for (int i = 0; i < 4; i++) {
			gammaData[i] = message.GetFloat(i);
		}
	}

	void OnReceiveDelta(OscMessage message) {
		for (int i = 0; i < 4; i++) {
			deltaData[i] = message.GetFloat(i);
		}
	}

	void OnReceiveTheta(OscMessage message) {
		for (int i = 0; i < 4; i++) {
			thetaData[i] = message.GetFloat(i);
		}
	}

	void OnReceiveAcc(OscMessage message) {
		for (int i = 0; i < 3; i++) {
			accData[i] = message.GetFloat(i);
		}
	}
}
