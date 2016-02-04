using UnityEngine;
using System.Collections;

public class NetworkObj : Photon.MonoBehaviour {
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;

    //AWAKE 네트워크로 인한 추가
    void Awake()
    {
        if (GetComponent<Player>() != null) GetComponent<Player>().Set_isControl(photonView.isMine);
        if (GetComponent<Enemy>() != null) correctPlayerPos = new Vector3(0f, 10f, 0f);
    }

    void Update()
    {
        if (photonView.isMine == true) return;
        float distance = Vector3.Distance(transform.position, this.correctPlayerPos);
        if (distance < 2f)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
        }
        else
        {
            transform.position = this.correctPlayerPos;
            transform.rotation = this.correctPlayerRot;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
