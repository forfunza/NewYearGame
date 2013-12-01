using UnityEngine;
using System.Collections;

public class RoomPage : Photon.MonoBehaviour {
	
	public bool isEnable = false;
	private string textFieldText = "";
	private string character;
	private string readyLabel = "Ready";
	private int readyStatus = 1;
	public GUISkin uiSkin;

	
	
	// Use this for initialization
	void Start () {
		PhotonNetwork.automaticallySyncScene = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		GUI.skin = uiSkin;
		if(isEnable){
			GUI.Window( 3 , new Rect(0   ,0  , 1024 ,768), 
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

		float currentHeight = 200;
		float x = 80;
		for(int index =0 ; index < PhotonNetwork.room.playerCount;index++){
			if(PhotonNetwork.playerList[index].name.Equals(PhotonNetwork.room.customProperties["Master"])){
				GUI.Box(new Rect(x,currentHeight,312,524),"");	
				if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Boy")){
					character = "Boy";
					GUI.Box(new Rect(x+20,currentHeight+120,277,366),"","BoyChar");
					
				}else if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Girl")){
					character = "Girl";			
					GUI.Box(new Rect(x+20,currentHeight+120,277,366),"","GirlChar");
				}
				GUI.Label(new Rect(x+100,currentHeight+40,200,30),PhotonNetwork.playerList[index].name+ " : " +character);
				x = x + 360;
			}else{
				x = 580;
				GUI.Box(new Rect(x,currentHeight,312,524),"");;	
				if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Boy")){
					character = "Boy";
					GUI.Box(new Rect(x+20,currentHeight+120,277,366),"","BoyChar");
				}else if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Girl")){
					character = "Girl";
					GUI.Box(new Rect(x+20,currentHeight+120,277,366),"","GirlChar");
				}
				GUI.Label(new Rect(x+100,currentHeight+40,200,30),PhotonNetwork.playerList[index].name+ " : " +character);

				x = x - 560;
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
				}else if(PhotonNetwork.playerList[index].customProperties["Ready"].Equals("0")){
					readyStatus = 0;
				}else if(PhotonNetwork.room.playerCount==1){
					readyStatus = 0;
				}else{
					readyStatus = 1;
				}


			}
			if(readyStatus == 1){
				if(GUI.Button(new Rect(150,580,242,66),"Start Game","Start")){
					PhotonNetwork.room.visible = false;
					PhotonNetwork.LoadLevel("GameThaiScene");	
				}
			}

		}else{
			if(GUI.Button(new Rect(600,580,242,66),readyLabel,"")){
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
