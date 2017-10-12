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
        maxVelocity = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {

		frontAsymmetry = (float)(OSCTest.eegData [1] - OSCTest.eegData [2]);
		Debug.Log(frontAsymmetry.ToString());
        frontAsymmetry = Math.Sign(frontAsymmetry);
        Debug.Log(frontAsymmetry.ToString());
		frontAsymmetry = (float) (frontAsymmetry * Math.Min(maxVelocity, Math.Max(1-OSCTest.GSRData/500, 0.1)));
        transform.Rotate(Vector3.up * frontAsymmetry);
	}
}
