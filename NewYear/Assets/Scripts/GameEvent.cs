using UnityEngine;
using System.Collections;

public class GameEvent : Photon.MonoBehaviour {

	public GameObject play1,play2;
	// Use this for initialization
	void Start () {
		if(PhotonNetwork.isMasterClient){
			print("tttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt");
			PhotonNetwork.Instantiate("First Person Controller",
		                          play1.transform.position,Quaternion.identity,0);
		}else{
			print("djkfsfnbegurgulrgwrghoirwlhfbjkrwbjkwbjkwbkjfwbgklfwekhfgkwgflwg");
			PhotonNetwork.Instantiate("First Person Controller",
			                          new Vector3(-1.768463f,-2.732378f,33.69859f),Quaternion.identity,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
