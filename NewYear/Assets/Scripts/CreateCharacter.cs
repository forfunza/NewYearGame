using UnityEngine;
using System.Collections;

public class CreateCharacter : Photon.MonoBehaviour {
	
	public bool isEnable = true;
	public string playerName ;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		if(isEnable){
			GUI.Window( 0 , new Rect(Screen.width / 2 - 200 , Screen.height /2 - 55 , 400 , 110), InsertName , "Login Page");
		}
	}
	
	void InsertName(int id){
		playerName = GUI.TextField( new Rect(20,35,360,30),playerName,16);


		if(GUI.Button(new Rect(125,70,150,30),"Login"))
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
		print(PhotonNetwork.playerName + " Connected ");
		SelectWorld selectWorld = gameObject.GetComponent<SelectWorld>();
		selectWorld.isEnable = true;
		isEnable = false;
	}
	
	void OnFailedToConnectedToPhoton(ExitGames.Client.Photon.StatusCode status){
		print(status + "FailedConnected");
		
	}
}
