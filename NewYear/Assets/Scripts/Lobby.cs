using UnityEngine;
using System.Collections;

public class Lobby : Photon.MonoBehaviour {

	public  bool isEnable = false;
	private string world;
	private string character;
	void OnGUI(){
		if(isEnable){
			if(PhotonNetwork.player.customProperties["WorldMap"].Equals("Thai")){
				world = "Thai";
			}else if(PhotonNetwork.player.customProperties["WorldMap"].Equals("Japan")){
				world = "Japan";
			}else if(PhotonNetwork.player.customProperties["WorldMap"].Equals("International")){
				world = "International";
			}
			GUI.Window( 2 , new Rect(Screen.width / 2 - 300 , Screen.height /2 - 250 , 600 ,500 ), 
			           LobbyUI , "Game Lobby : World " + world);
		}
		
	}
	
	void LobbyUI(int id){

		if(GUI.Button(new Rect(15 , 450 ,50,30),"Back")){
			SelectWorld selectWorld = gameObject.GetComponent<SelectWorld>();
			selectWorld.isEnable = true;
			isEnable = false;
		}


		GUI.Label (new Rect(150,30,200,30),"World : "+world);
		GUI.Box(new Rect(15,50,370,380),"");
		if(GUI.Button(new Rect(400,360,70,70),"Boy")){
			character = "Boy";
			ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
			custom.Add("Character",character);
			PhotonNetwork.player.SetCustomProperties(custom);
			print("Character : "+PhotonNetwork.player.customProperties["Character"]);
		}

		if(GUI.Button(new Rect(500,360,70,70),"Girl")){
			character = "Girl";
			ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
			custom.Add("Character",character);
			PhotonNetwork.player.SetCustomProperties(custom);
			print("Character : "+PhotonNetwork.player.customProperties["Character"]);
		}
		
		if(GUI.Button(new Rect(200,455,200,30),"Create Room")){
			CreateRoom createRoom = gameObject.GetComponent<CreateRoom>();
			createRoom.isEnable = true;
			isEnable = false;
		}
		
		RoomInfo[] gameRoomList = PhotonNetwork.GetRoomList();
		float currentHeight = 65;
		int i = 0;
		if(gameRoomList.Length > 0){
			foreach(RoomInfo gameRoom in gameRoomList)
			{
				print("player : "+PhotonNetwork.player.customProperties["WorldMap"] + " Room : " + gameRoom.customProperties["WorldMap"]);
				if(PhotonNetwork.player.customProperties["WorldMap"].Equals(gameRoom.customProperties["WorldMap"])){
					if(GUI.Button(new Rect(30,currentHeight,330,30),gameRoom.name+"  "+
					              gameRoom.playerCount+"/"+gameRoom.maxPlayers+" Host : "+gameRoom.customProperties["Master"])){
						PhotonNetwork.JoinRoom(gameRoom.name);
					}
					currentHeight = currentHeight + 40;
				}else{
					GUI.Label(new Rect(150,225,600,320),"No Room Available...");
				}
			}
		}else{
			GUI.Label(new Rect(150,225,600,320),"No Room Available...");
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
