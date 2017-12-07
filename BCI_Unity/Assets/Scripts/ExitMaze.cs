using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitMaze : MonoBehaviour {

	public Text youWinText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {

			float currentTime = Time.time;

			youWinText.text = youWinText.text + "\n Your time is " + Mathf.Floor (currentTime / 60).ToString() + ":" + ((int) (currentTime % 60)).ToString();
			youWinText.enabled = true;
			Time.timeScale = 0;

		}
	}
}
