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

		frontAsymmetry = (float)(OSCTest.alphaData [1] - OSCTest.alphaData [2]);
		Debug.Log(frontAsymmetry.ToString());
        frontAsymmetry = Math.Sign(frontAsymmetry);
        Debug.Log(frontAsymmetry.ToString());
		frontAsymmetry = (float) (frontAsymmetry * maxVelocity);
        transform.Rotate(Vector3.up * frontAsymmetry);

	}


}
