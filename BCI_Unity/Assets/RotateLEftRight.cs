using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLEftRight : MonoBehaviour {

	private double frontAsymmetry;

	// Use this for initialization
	void Start () {
		frontAsymmetry = 0;
	}
	
	// Update is called once per frame
	void Update () {
		frontAsymmetry = OSCTest.eegData [3] - OSCTest.eegData [2];
		transform.Translate((float) frontAsymmetry, 0, 0);
		
	}
}
