using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.UI;

public class Roundmanager : Photon.MonoBehaviour {

	ScoreManager scoreManager;
	
	[SerializeField] Text bluetext;
	
	float detime;
	
	public static JSONObject arduinodata;

	string encodingtext;
	


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
		foreach (PhotonPlayer player in PhotonNetwork.playerList) {
			if(player.name.ToString() != "")
			{
				scoreManager.SetScore (player.name, "kills", player.GetKill());
				scoreManager.SetScore (player.name, "deaths", player.GetDeath());
				scoreManager.SetScore (player.name, "assist", (int)player.GetKill()-(int)player.GetDeath()); //score
			}
		}

	}



		
}
