using UnityEngine;
using System.Collections;

public class SelectWorld : Photon.MonoBehaviour {
	public bool isEnable = false;
	public string selectWorld ;
	// Use this for initialization
	void Start () {
		selectWorld = "Thai";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(isEnable){
			GUI.Window( 1 , new Rect(Screen.width / 2 -380 , Screen.height /2 - 280  , 760 , 560), WorldMap , "Select World");
		}
	}

	void WorldMap(int id){

		if(GUI.Button(new Rect(50,50,190,370),"Thai"))
		{
			selectWorld = "Thai";
			print(selectWorld);
		}
		if(GUI.Button(new Rect(280,50,190,370),"Japan"))
		{
			selectWorld = "Japan";
			print(selectWorld);
		}
		if(GUI.Button(new Rect(510,50,190,370),"International"))
		{
			selectWorld = "International";
			print(selectWorld);
		}

		if(GUI.Button(new Rect(325,450,100,50),"Enter"))
		{
			ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
			custom.Add("WorldMap",selectWorld);
			PhotonNetwork.player.SetCustomProperties(custom);
			print("World Select : "+PhotonNetwork.player.customProperties["WorldMap"]);
			Lobby lobby = gameObject.GetComponent<Lobby>();
			lobby.isEnable = true;
			isEnable = false;
		}

	}
}
