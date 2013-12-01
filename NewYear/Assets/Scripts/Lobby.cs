using UnityEngine;
using System.Collections;

public class Lobby : Photon.MonoBehaviour {

	public  bool isEnable = false;
	private string world;
	private string character;
	public GUISkin uiSkin;
	void OnGUI(){
		if(isEnable){
			GUI.skin = uiSkin;

			GUI.Window( 2 , new Rect(0   ,0  , 1024 ,768), 
			           LobbyUI , "");
		}
		
	}
	
	void LobbyUI(int id){

		if(GUI.Button(new Rect(15 , 450 ,50,30),"Back")){
			SelectWorld selectWorld = gameObject.GetComponent<SelectWorld>();
			selectWorld.isEnable = true;
			isEnable = false;
		}

		if((PhotonNetwork.player.customProperties["Character"]).Equals("Boy")){
			GUI.Box(new Rect(670,230,277,366),"","BoyChar");
		}else{
			GUI.Box(new Rect(670,230,277,366),"","GirlChar");
		}


;
		GUI.Box(new Rect(30,170,621,524),"");
		GUI.Box(new Rect(680,170,256,512),"","CharacterBox");
		if(GUI.Button(new Rect(680,660,140,66),"","Button1")){
			character = "Boy";
			ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
			custom.Add("Character",character);
			PhotonNetwork.player.SetCustomProperties(custom);
			print("Character : "+PhotonNetwork.player.customProperties["Character"]);
		}

		if(GUI.Button(new Rect(820,660,140,66),"","Button2")){
			character = "Girl";
			ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
			custom.Add("Character",character);
			PhotonNetwork.player.SetCustomProperties(custom);
			print("Character : "+PhotonNetwork.player.customProperties["Character"]);
		}
		
		if(GUI.Button(new Rect(185,660,332,55),"","Button3")){
			CreateRoom createRoom = gameObject.GetComponent<CreateRoom>();
			createRoom.isEnable = true;
			isEnable = false;
		}
		
		RoomInfo[] gameRoomList = PhotonNetwork.GetRoomList();
		float currentHeight = 250;
		int i = 0;
		if(gameRoomList.Length > 0){
			foreach(RoomInfo gameRoom in gameRoomList)
			{
		
					if(GUI.Button(new Rect(40,currentHeight,587,50),gameRoom.name+"  "+
					              gameRoom.playerCount+"/"+gameRoom.maxPlayers+" Host : "+gameRoom.customProperties["Master"],"RoomButton")){
						PhotonNetwork.JoinRoom(gameRoom.name);
					}
					currentHeight = currentHeight + 70;

			}
		}else{

		}
	}
	
	void OnJoinedRoom(){
		print("Join "+ PhotonNetwork.room.name);
		RoomPage roomPage = gameObject.GetComponent<RoomPage>();
		roomPage.isEnable = true;

		isEnable = false;
	}

	// Use this for initialization
	void Start () {
		character = "Boy";
		ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
		custom.Add("Character",character);
		PhotonNetwork.player.SetCustomProperties(custom);
		print("Character : "+PhotonNetwork.player.customProperties["Character"]);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
