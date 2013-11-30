using UnityEngine;
using System.Collections;

public class CreateRoom : Photon.MonoBehaviour {
	public bool isEnable = true;
	public string roomName;
	private string world;

	// Use this for initialization
	void Start () {

	}
	void OnGUI(){
		if(isEnable){
			GUI.Window( 3 , new Rect(Screen.width / 2 - 200 , Screen.height /2 - 55 , 400 , 110), Room , "Create Room");
		}
	}
	void Room(int id){
		roomName = GUI.TextField( new Rect(20,35,360,30),roomName,16);

		if(GUI.Button(new Rect(125,70,150,30),"Create"))
		{
			if(PhotonNetwork.player.customProperties["WorldMap"].Equals("Thai")){
				world = "Thai";
			}else if(PhotonNetwork.player.customProperties["WorldMap"].Equals("Japan")){
				world = "Japan";
			}else if(PhotonNetwork.player.customProperties["WorldMap"].Equals("International")){
				world = "International";
			}
			ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
			custom.Add("Master",PhotonNetwork.player.name);
			custom.Add("WorldMap",world);
			string[] customName = new string[]{"Master","WorldMap"};
			PhotonNetwork.CreateRoom(roomName,true,true,2,custom,customName);
	
		}
	}

	void OnCreatedRoom(){



		print("Room Created : "+ PhotonNetwork.room.name +"  "+PhotonNetwork.room.customProperties["WorldMap"]+ " Player : "+PhotonNetwork.player.ID);	

		isEnable = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
