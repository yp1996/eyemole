using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class changeColour : MonoBehaviour {

    private Color newColour;
    private float skyColour;

	// Use this for initialization
	void Start () {
        newColour = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        skyColour = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(OSCTest.accData[0].ToString());
        skyColour = (float) Math.Log(OSCTest.alphaData[1] / OSCTest.alphaData[2]) / ((float) (Math.Log(OSCTest.alphaData[1]) + Math.Log(OSCTest.alphaData[2])));
        Debug.Log(skyColour.ToString());
        newColour = new Color(Math.Abs(skyColour), Math.Abs(skyColour), Math.Abs(skyColour), 1.0f);
        GetComponent<Renderer>().material.SetColor("_Color", newColour);
    }
}
