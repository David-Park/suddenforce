using Photon;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class playerstatus : Photon.MonoBehaviour {

	public delegate void Respawn(float time);
	public event Respawn RespawnMe;
	public delegate void SendMessage(string MessageOverlay);
	public event SendMessage SendNetworkMessage;


	public AudioClip damagesound;
	private GameObject particle;

	float health = 100f;

    // Use this for initialization
    void Start () {

		if (photonView.isMine) 
		{
			mainscreen.health = health;
			GetComponent<PlayerShoot> ().enabled = true;
		} 
    }
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (health);
		}
		else
		{
			health = (float)stream.ReceiveNext();
		}
	}
	
	[PunRPC]
	public void GetShot(float damage, PhotonPlayer enemyPlayer)
	{
			health -= damage;

			if (photonView.isMine) {
			
			particle = GameObject.Find("spPoint");
			AudioSource.PlayClipAtPoint(damagesound, particle.transform.position);
			mainscreen.health = health;
			}

			if (health <= 0 && photonView.isMine) {

				//if(SendNetworkMessage != null)
				//	SendNetworkMessage(PhotonNetwork.player.name + " was killed by " + enemyName);

				enemyPlayer.AddKill(1);
				PhotonNetwork.player.AddDeath(1);
				
				if(RespawnMe != null)
					RespawnMe(3f);
				
				GameObject.Find("screenset").SetActive(false);
				PhotonNetwork.Destroy (gameObject);
				
			}
	}

	[PunRPC]
	public void Gethealth()
	{
		health = 100f;
		mainscreen.health = 100f;
	}
}
