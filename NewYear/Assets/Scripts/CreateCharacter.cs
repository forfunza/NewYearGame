using UnityEngine;
using System.Collections;

public class CreateCharacter : Photon.MonoBehaviour {
	
	public bool isEnable = true;
	public string playerName;
	public GUISkin uiSkin;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		if(isEnable){
			GUI.skin = uiSkin;
			GUI.Window( 0 , new Rect(0   ,0  , 1024 ,768), InsertName , "");
		}
	}
	
	void InsertName(int id){
		playerName = GUI.TextField( new Rect(295,450,433,61),playerName,16);


		if(GUI.Button(new Rect(355,540,321,111),""))
		{
		
			if(playerName == "" || playerName == null)
			{
				playerName = "Guest " + Random.Range(0, 10000);
			}

			PhotonNetwork.playerName = playerName;
			PhotonNetwork.Connect(PhotonNetwork.PhotonServerSettings.ServerAddress,PhotonNetwork.PhotonServerSettings.ServerPort
			                      ,PhotonNetwork.PhotonServerSettings.AppID,"0.1");
		}
	}
	
	void OnConnectedToPhoton(){
		print(PhotonNetwork.playerName + " Connected ");;
		Lobby lobby = gameObject.GetComponent<Lobby>();
		lobby.isEnable = true;
		isEnable = false;
	}
	
	void OnFailedToConnectedToPhoton(ExitGames.Client.Photon.StatusCode status){
		print(status + "FailedConnected");
		
	}
}
