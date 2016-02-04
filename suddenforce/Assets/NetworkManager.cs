using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour 
{
	[SerializeField] Text connectionText;
	[SerializeField] GameObject serverWindow;
	[SerializeField] InputField username;
	[SerializeField] InputField roomName;
	[SerializeField] Text roundtimetext;
	[SerializeField] GameObject screenset;
	[SerializeField] Text textRespawnTime;
	[SerializeField] GameObject scoreboard;
	[SerializeField] Text roundtimetextR;
	[SerializeField] Text textRespawnTimeR;
	[SerializeField] Text debugtext;


	GameObject player;
	Queue<string> messages;
	const int messageCount = 6;
	PhotonView photonView;
	public float roundtime = 0.0f;
	public bool flag=true;
	float tmp;
	string fromArduino;
	public static string arduinotext;

	
	void Start () 
	{

		
		BtConnector.moduleMAC("20:15:07:02:69:59");
		
		if (!BtConnector.isBluetoothEnabled ()) {
			BtConnector.askEnableBluetooth ();
		} else {
			BtConnector.moduleMAC ("20:15:07:02:69:59");
			BtConnector.connect ();
		}


		photonView = GetComponent<PhotonView> ();
		messages = new Queue<string> (messageCount);
		
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		PhotonNetwork.ConnectUsingSettings ("1.0");
		StartCoroutine ("UpdateConnectionString");
	}

	IEnumerator UpdateConnectionString () 
	{
		while(true)
		{
			connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();

			yield return null;
		}
	}

	void Update()
	{
		arduinotext = BtConnector.readLine ();

		if (PhotonNetwork.connected && PhotonNetwork.isMasterClient && flag) {
			roundtime -= Time.smoothDeltaTime;
			roundtimetext.text = roundtime.ToString("N0");
			roundtimetextR.text = roundtimetext.text;
			ViewRoundTime(roundtime.ToString("N0"));
			if(roundtime < 0.0f)
			{
				ViewScore();
				PhotonNetwork.DestroyAll();
				flag=false;
			}
		}
	}

	void OnJoinedLobby()
	{
		serverWindow.SetActive (true);
	}

	
	public void JoinRoom()
	{
		PhotonNetwork.player.name = username.text;
		RoomOptions roomOptions = new RoomOptions(){ isVisible = true, maxPlayers = 10 };PhotonNetwork.JoinOrCreateRoom (roomName.text, roomOptions, TypedLobby.Default);
	}
	
	void OnJoinedRoom()
	{
		serverWindow.SetActive (false);
		StopCoroutine ("UpdateConnectionString");
		connectionText.text = "";
		roundtime = 550.0f;
		flag = true;
		StartSpawnProcess (0f);
	}
	
	void StartSpawnProcess(float respawnTime)
	{
		StartCoroutine ("SpawnPlayer", respawnTime);
	}
	
	IEnumerator SpawnPlayer(float respawnTime)
	{
		textRespawnTime.text = "Respawn..";
		textRespawnTimeR.text = textRespawnTime.text;
		yield return new WaitForSeconds(respawnTime);
		textRespawnTime.text = "";
		textRespawnTimeR.text = textRespawnTime.text;
		screenset.SetActive (true);
		int index = Random.Range (0, 10);
		Vector3 position = new Vector3(-2, 0, 0);
		position.x += Random.Range(-3f, 3f);
		position.z += Random.Range(-4f, 4f);
		GameObject newPlayerObject = PhotonNetwork.Instantiate("playerscore", position, Quaternion.identity, 0);
		newPlayerObject.GetComponent<playerstatus> ().RespawnMe += StartSpawnProcess;
		player.GetComponent<playerstatus> ().SendNetworkMessage += AddMessage;
	}

	
	void AddMessage(string message)
	{
		photonView.RPC ("AddMessage_RPC", PhotonTargets.All, message);
	}
	void ViewRoundTime(string message)
	{
		photonView.RPC ("ViewRoundTime_RPC", PhotonTargets.All, message);
	}
	void ViewScore()
	{
		photonView.RPC ("ViewScore_RPC", PhotonTargets.All);
	}

	public void UIcreate()
	{
		Instantiate (scoreboard);
	}
	
	[PunRPC]
	void AddMessage_RPC(string message)
	{
		messages.Enqueue (message);
		if(messages.Count > messageCount)
			messages.Dequeue();
		
		roundtimetext.text = "";
		foreach(string m in messages)
			roundtimetext.text += m + "\n";
	}

	[PunRPC]
	void ViewRoundTime_RPC(string message)
	{
		roundtimetext.text = message;
		roundtimetextR.text = roundtimetext.text;
	}

	[PunRPC]
	void ViewScore_RPC()
	{
		GameObject.Find ("screenset").SetActive (false);
		Instantiate (scoreboard);
	}

}