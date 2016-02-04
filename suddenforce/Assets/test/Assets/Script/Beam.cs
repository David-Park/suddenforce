using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

    private float speed = 10f;

	// Update is called once per frame
	void Update () {
        Moving();
        AutoDie();
	}

    void Moving()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void AutoDie()
    {
        if (transform.position.y < 6f) return;
        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        Destroy(gameObject);
        return null;
    }
}
