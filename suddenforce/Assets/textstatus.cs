using UnityEngine;
using System.Collections;

public class textstatus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 100, 20), "Hello World!");
    }
}
