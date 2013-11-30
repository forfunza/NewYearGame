using UnityEngine;
using System.Collections;

public class GameEvent : Photon.MonoBehaviour {

	public GameObject play1,play2;
	// Use this for initialization
	void Start () {
		if(PhotonNetwork.isMasterClient){
			PhotonNetwork.Instantiate("First Person Controller",
		                          new Vector3(-1.768463f,-2.732378f,30.58857f),Quaternion.identity,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
