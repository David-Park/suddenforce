using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    private float Dropspeed = 0f;


	// Update is called once per frame
	void Update () {
        Dropit();
        CheckAutoDie();
	}

    void Dropit()
    {
        transform.Translate(new Vector3(0f, -1f, 0f) * Dropspeed * Time.deltaTime);
    }

    void CheckAutoDie()
    {
        if (transform.position.y > -5f) return;
        StartCoroutine("Die");
    }

    public void Set_Speed(float x)
    {
        Dropspeed = x;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "beam") return;
        StartCoroutine("Die"); 
    }

    IEnumerator Die()
    {
        if (PhotonNetwork.isMasterClient) PhotonNetwork.Destroy(gameObject);
        //if (!PhotonNetwork.isMasterClient) Destroy(gameObject);
        return null;
    }
}
