using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shoot : MonoBehaviour {

	public static GameObject spPoint;

	// Use this for initialization
	void Start () {
		spPoint = GameObject.Find("spPoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
