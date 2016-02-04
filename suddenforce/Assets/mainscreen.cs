using Photon;
using UnityEngine;
using System.Collections;

public class mainscreen : PunBehaviour {
	

    #region Properties
    public GUISkin suddensui;
    public static float health = 100f;
    public static float bullet = 100f;
    public static float guntype = 2f;

    #endregion

    #region Members
    
    #endregion

    #region Unity
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        GUI.skin = suddensui;


        ///GUI.TextArea(new Rect(Screen.width / 10, Screen.height / 2, 200, 50), "dfasdf");
                
    }

	void OnCardboardGUI()
	{

	}

    #endregion

    #region Photon
    public override void OnJoinedRoom()
    {
        CreatePlayerObject();
    }

    private void CreatePlayerObject()
    {
        
    }
    #endregion
}
