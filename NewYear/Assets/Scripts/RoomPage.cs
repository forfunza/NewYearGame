using UnityEngine;
using System.Collections;

public class RoomPage : Photon.MonoBehaviour {
	
	public bool isEnable = false;
	private string textFieldText = "";
	private string character;
	
	
	// Use this for initialization
	void Start () {
		
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

		GUI.Box(new Rect(15,60,570,180),"");
		float currentHeight = 75;

		for(int index =0 ; index < PhotonNetwork.room.playerCount;index++){
			GUI.Button(new Rect(20,currentHeight,400,30),PhotonNetwork.playerList[index].name);	
			if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Boy")){
				character = "Boy";
			}else if(PhotonNetwork.playerList[index].customProperties["Character"].Equals("Girl")){
				character = "Girl";
			}
			GUI.Button(new Rect(425,currentHeight,155,30),character);
			currentHeight = currentHeight + 40;
		}
		
		if(PhotonNetwork.isMasterClient){
			if(GUI.Button(new Rect(200,455,200,30),"Start Game")){
				photonView.RPC("StartGame",PhotonTargets.All);
			}
		}
		

	}

}
