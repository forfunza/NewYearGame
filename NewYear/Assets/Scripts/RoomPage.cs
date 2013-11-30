using UnityEngine;
using System.Collections;

public class RoomPage : Photon.MonoBehaviour {
	
	public bool isEnable = false;
	private string textFieldText = "";
	private string character;
	private string readyLabel = "Ready";
	private int readyStatus = 1;
	
	
	// Use this for initialization
	void Start () {
		PhotonNetwork.automaticallySyncScene = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){

		if(isEnable){
			GUI.Window( 3 , new Rect(Screen.width / 2 - 300 , Screen.height /2 - 250 , 600 ,500 ), 
			           RoomUI , PhotonNetwork.room.name);
		}
	}
	
	void RoomUI(int id){

		if(GUI.Button(new Rect(10 , 450 ,50,30),"Back")){
			Lobby lobby = gameObject.GetComponent<Lobby>();
			lobby.isEnable = true;
			isEnable = false;
			PhotonNetwork.LeaveRoom();
		}

		float currentHeight = 75;
		float x = 20;
		for(int index =0 ; index < PhotonNetwork.room.playerCount;index++){
			if(PhotonNetwork.playerList[index].name.Equals(PhotonNetwork.room.customProperties["Master"])){
				GUI.Button(new Rect(x,currentHeight,200,300),PhotonNetwork.playerList[index].name);	
				if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Boy")){
					character = "Boy";
				}else if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Girl")){
					character = "Girl";
				}
				GUI.Button(new Rect(x,currentHeight+310,200,30),PhotonNetwork.playerList[index].name+ " : " +character);
				x = x + 360;
			}else{
				x = 380;
				GUI.Button(new Rect(x,currentHeight,200,300),PhotonNetwork.playerList[index].name);	
				if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Boy")){
					character = "Boy";
				}else if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Girl")){
					character = "Girl";
				}
				GUI.Button(new Rect(x,currentHeight+310,200,30),PhotonNetwork.playerList[index].name+ " : " +character);
				x = x - 360;
			}
		}

		ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
		
		if(PhotonNetwork.isMasterClient){

			custom.Add("Ready","1");
			PhotonNetwork.player.SetCustomProperties(custom);
			custom.Clear();

			for(int index =0 ; index < PhotonNetwork.room.playerCount;index++){
				if(PhotonNetwork.playerList[index].customProperties["Ready"]==null){
					custom.Add("Ready","0");
					PhotonNetwork.player.SetCustomProperties(custom);
					custom.Clear();
					readyStatus = 0;
				}else if(PhotonNetwork.room.playerCount==1){
					readyStatus = 0;
				}else{
					readyStatus = 1;
				}

			}
			if(readyStatus == 1){
				if(GUI.Button(new Rect(200,455,200,30),"Start Game")){
					PhotonNetwork.room.visible = false;
					PhotonNetwork.LoadLevel("GameThaiScene");	
				}
			}

		}else{
			if(GUI.Button(new Rect(200,455,200,30),readyLabel)){
				readyLabel = "Wait !!";
				custom.Add("Ready","1");
				PhotonNetwork.player.SetCustomProperties(custom);
				custom.Clear();
				print(PhotonNetwork.player.customProperties["Ready"]);
			}
		}
		

	}

	void OnPhotonPlayerDisconnected(PhotonPlayer player){
		if(PhotonNetwork.isMasterClient){
			PhotonNetwork.LeaveRoom();
		}

		Lobby lobby = gameObject.GetComponent<Lobby>();
		lobby.isEnable = true;
		isEnable = false;
	}
	
	void StartGame(){
		PhotonNetwork.room.visible = false;
		PhotonNetwork.LoadLevel("GameThaiScene");	
	}
	
}
