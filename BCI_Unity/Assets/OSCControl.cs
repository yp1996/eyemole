using UnityEngine;
using System.Collections;

public class OSCControl : MonoBehaviour {
    
   	public OSC osc;


	// Use this for initialization
	void Start () {
       osc.SetAddressHandler( "/muse/eeg" , OnReceiveEEG );
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnReceiveEEG(OscMessage message){
		float x = message.GetFloat(0);
         float y = message.GetFloat(1);
		float z = message.GetFloat(2);
        float tp10 = message.GetFloat(3);
        Debug.Log (x.ToString());
        Debug.Log (tp10.ToString());
	}



}