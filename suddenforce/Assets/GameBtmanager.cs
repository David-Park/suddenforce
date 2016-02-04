using UnityEngine;
using System.Collections;


public class GameBtmanager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.TextArea (new Rect (Screen.width - 500, 0, 500, 100), BtConnector.readControlData ());
		GUI.TextArea (new Rect (Screen.width - 500, 100, 500, 100), BtConnector.readLine ());
		GUI.TextArea (new Rect (Screen.width - 500, 200, 500, 100), BtConnector.read());
	}
}
