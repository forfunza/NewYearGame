using UnityEngine;
using System.Collections;

public class CreateRoom : Photon.MonoBehaviour {
	public bool isEnable = true;
	public string roomName;
	private string world;
	public GUISkin uiSkin;

	// Use this for initialization
	void Start () {

	}
	void OnGUI(){
		if(isEnable){
			GUI.skin = uiSkin;
			GUI.Window( 3 , new Rect(0   ,0  , 1024 ,768), Room , "");
		}
	}
	void Room(int id){
		roomName = GUI.TextField( new Rect(270,390,490,71),roomName,16);
		GUI.Box( new Rect(250,300,564,228),"");
		if(GUI.Button(new Rect(400,500,238,62),""))
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
