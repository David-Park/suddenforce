using UnityEngine;
using System.Collections;

public class bpanel : MonoBehaviour {

    private GameObject bloodpanel;

	void Awake () {
        bloodpanel = GameObject.Find("Panel");
        bloodpanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
            StartCoroutine(PlayWait());
    }
	
    IEnumerator PlayWait()
    {
        bloodpanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        bloodpanel.SetActive(false);
    }
    
}
