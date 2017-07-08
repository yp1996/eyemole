using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {

	private float frontAsymmetry;

	// Use this for initialization
	void Start () {
		frontAsymmetry = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		frontAsymmetry = (float)(OSCTest.alphaData [1] - OSCTest.alphaData [2]);
		Debug.Log (frontAsymmetry.ToString ());
		transform.Translate(Vector3.right * frontAsymmetry);
	}
}
