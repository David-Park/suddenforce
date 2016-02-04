using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

    public GameObject beam;
    
    private float fire_last = 0f;
    private float fire_term = 0.3f;
    private float Speed = 10f;
    
    //이하 네트워크로 인한 추가
    public bool isControl = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Fire();
        if (isControl != true) return;
        if (Input.GetKey(KeyCode.RightArrow)) Move_Right();
        if (Input.GetKey(KeyCode.LeftArrow)) Move_Left();
	}

    void Move_Right()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
        if (transform.position.x > 5f) transform.position = new Vector3(5f, 0f, 0f);
    }

    void Move_Left()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
        if (transform.position.x < -5f) transform.position = new Vector3(-5f, 0f, 0f);
    }

    void Fire()
    {
        if (fire_last + fire_term > Time.time) return;
        StartCoroutine("Fire_Beam");
        fire_last = Time.time;
    }

    IEnumerator Fire_Beam()
    {
        Instantiate(beam, transform.position, Quaternion.identity);
        return null;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag != "enemy") return;
        PhotonNetwork.Destroy(gameObject);
    }

    public void Set_isControl(bool x)
    {
        isControl = x;
    }
}
