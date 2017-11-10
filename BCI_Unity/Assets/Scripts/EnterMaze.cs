using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterMaze : MonoBehaviour {

	public Text gameOverText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {

			gameOverText.enabled = true;
			Time.timeScale = 0;

		}
	}
}
