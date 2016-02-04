using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class PlayerShoot : Photon.MonoBehaviour {
	public GUISkin suddengui;
	bool shooting = false;
	int i=0;
	public Transform explotion;
	public AudioClip sound;
	private GameObject particle;
	string fromarduino;
	JSONObject arduinojson;
	
	public static float damage = 3f;
	public static float bullet = 100f;
	public static float guntype = 2f;


	// Use this for initialization
	void Start () {
		mainscreen.bullet = bullet;
	}
	
	// Update is called once per frame
	void Update () {
		fromarduino = NetworkManager.arduinotext;
		arduinojson = new JSONObject (fromarduino);

		if (arduinojson[1].ToString() == "\"true\"" && bullet >= 0.0f) {
			particle = GameObject.Find("spPoint");
			Instantiate(explotion, particle.transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(sound, particle.transform.position);

			bullet -= 1.0f;
			mainscreen.bullet = bullet;


			if(Vuforia.AmmoTrakerableEventHandler.trackable ==true && Vuforia.AmmoTrakerableEventHandler.flag == false)
			{
				Vuforia.AmmoTrakerableEventHandler.flag = true;
				bullet = 100f;
				mainscreen.bullet = bullet;
				
				GameObject.Find("AmmoBox").GetComponent<Renderer>().enabled = false;
				GameObject.Find("AmmoBox").GetComponent<Collider>().enabled = false;
				
			}
			
			if(Vuforia.Gun1TrackableEventHandler.trackable == true && Vuforia.Gun1TrackableEventHandler.flag == false)
			{
				Vuforia.Gun1TrackableEventHandler.flag = true;
				damage = 10.0f;
				guntype = 3.0f;
				mainscreen.guntype = guntype;
				
				GameObject.Find("MARMO3").GetComponent<Renderer>().enabled = false;
				GameObject.Find("MARMO3").GetComponent<Collider>().enabled = false;	
				GameObject.Find("useditem1").GetComponent<Renderer>().enabled = true;
				GameObject.Find("useditem1").GetComponent<Collider>().enabled = true;
			}
			
			if(Vuforia.Gun2TrackableEventHandler.trackable ==true && Vuforia.Gun2TrackableEventHandler.flag == false)
			{
				Vuforia.Gun2TrackableEventHandler.flag = true;
				damage = 12.0f;
				guntype = 4.0f;
				mainscreen.guntype = guntype;
				
				GameObject.Find("ak47").GetComponent<Renderer>().enabled = false;
				GameObject.Find("ak47").GetComponent<Collider>().enabled = false;	
			}
			
			
			if(Vuforia.HealthTrackableEventHandler.trackable ==true && Vuforia.HealthTrackableEventHandler.flag == false)
			{
				Vuforia.HealthTrackableEventHandler.flag = true;
				
				mainscreen.bullet = bullet;
				
				this.photonView.RPC("Gethealth",PhotonNetwork.player);
				
				GameObject.Find("MedicalBox").GetComponent<Renderer>().enabled = false;
				GameObject.Find("MedicalBox").GetComponent<Collider>().enabled = false;	
				GameObject.Find("useditem4").GetComponent<Renderer>().enabled = true;
				GameObject.Find("useditem4").GetComponent<Collider>().enabled = true;
			}

		} // ㅊㅗㅇㅆㅗㄹㄸㅒ

		if (arduinojson[0].ToString() != "\"0\"" && mainscreen.health != 0.0f) {
			this.photonView.RPC("GetShot", PhotonTargets.All, damage, PhotonNetwork.player);//ㅈㅏㄱㅣㅈㅏㅅㅣㄴ
		}



	}

	void OnGUI(){
		i = 0;
		GUI.skin = suddengui;

		return;

		foreach (PhotonPlayer pplayer in PhotonNetwork.playerList) {
			if (GUI.Button (new Rect (0, 0+200*i, 200, 200), "Player "+ pplayer.name + " Shoot")) {
				//PhotonView.Find(pplayer.ID).gameObject.GetComponent<PhotonView>().RPC("GetShot", PhotonTargets.All, damage, PhotonNetwork.player.name);

				PhotonView.Find(pplayer.GetDeath()+1 + (pplayer.ID * 1000)).RPC("GetShot", PhotonTargets.All, damage, PhotonNetwork.player);
				if(photonView.isMine)
				{
					particle = GameObject.Find("spPoint");
					Instantiate(explotion, particle.transform.position, Quaternion.identity);
					AudioSource.PlayClipAtPoint(sound, particle.transform.position);
					
					bullet -= 1.0f;
					mainscreen.bullet = bullet;
				}

				if(Vuforia.AmmoTrakerableEventHandler.trackable ==true && Vuforia.AmmoTrakerableEventHandler.flag == false)
				{
					Vuforia.AmmoTrakerableEventHandler.flag = true;
					bullet = 100f;
					mainscreen.bullet = bullet;

					GameObject.Find("AmmoBox").GetComponent<Renderer>().enabled = false;
					GameObject.Find("AmmoBox").GetComponent<Collider>().enabled = false;

				}
				
				if(Vuforia.Gun1TrackableEventHandler.trackable == true && Vuforia.Gun1TrackableEventHandler.flag == false)
				{
					Vuforia.Gun1TrackableEventHandler.flag = true;
					damage = 10.0f;
					guntype = 3.0f;
					mainscreen.guntype = guntype;
					
					GameObject.Find("MARMO3").GetComponent<Renderer>().enabled = false;
					GameObject.Find("MARMO3").GetComponent<Collider>().enabled = false;	
					GameObject.Find("useditem1").GetComponent<Renderer>().enabled = true;
					GameObject.Find("useditem1").GetComponent<Collider>().enabled = true;
				}

				if(Vuforia.Gun2TrackableEventHandler.trackable ==true && Vuforia.Gun2TrackableEventHandler.flag == false)
				{
					Vuforia.Gun2TrackableEventHandler.flag = true;
					damage = 12.0f;
					guntype = 4.0f;
					mainscreen.guntype = guntype;
					
					GameObject.Find("ak47").GetComponent<Renderer>().enabled = false;
					GameObject.Find("ak47").GetComponent<Collider>().enabled = false;	
				}

				
				if(Vuforia.HealthTrackableEventHandler.trackable ==true && Vuforia.HealthTrackableEventHandler.flag == false)
				{
					Vuforia.HealthTrackableEventHandler.flag = true;

					mainscreen.bullet = bullet;

					this.photonView.RPC("Gethealth",PhotonNetwork.player);

					GameObject.Find("MedicalBox").GetComponent<Renderer>().enabled = false;
					GameObject.Find("MedicalBox").GetComponent<Collider>().enabled = false;	
					GameObject.Find("useditem4").GetComponent<Renderer>().enabled = true;
					GameObject.Find("useditem4").GetComponent<Collider>().enabled = true;
				}

			}


			i++;
		}

	}

}

