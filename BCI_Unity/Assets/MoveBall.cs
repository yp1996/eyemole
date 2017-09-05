using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {

	private float frontAsymmetry;
    private float maxVelocity;

	// Use this for initialization
	void Start () {
		frontAsymmetry = 0;
        maxVelocity = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		frontAsymmetry = (float)(OSCTest.eegData [1] - OSCTest.eegData [2]);
		Debug.Log(frontAsymmetry.ToString());
        frontAsymmetry = Math.Min(frontAsymmetry, maxVelocity);
        frontAsymmetry = Math.Max(frontAsymmetry, -maxVelocity);
        Debug.Log(frontAsymmetry.ToString());
        transform.Rotate(Vector3.up * frontAsymmetry);
	}
}
