using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private Text txt;

	// Use this for initialization
	void Start () {
		txt = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		float currentTime = Time.time;
		txt.text = "TIME: " + Mathf.Floor (currentTime / 60).ToString() + ":" + ((int) (currentTime % 60)).ToString();
		
	}
}
