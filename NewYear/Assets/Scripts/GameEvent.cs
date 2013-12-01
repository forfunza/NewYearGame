using UnityEngine;
using System.Collections;

public class GameEvent : Photon.MonoBehaviour {

	public GameObject play1,play2;
	// Use this for initialization
	void Start () {
		if(PhotonNetwork.isMasterClient){

			PhotonNetwork.Instantiate("First Person Controller",
		                          play1.transform.position,Quaternion.identity,0);
		}else{
			PhotonNetwork.Instantiate("Second Person Controller",
			                          play2.transform.position,play2.transform.rotation,0);
		}


	}

	// Update is called once per frame
	void Update () {
	
	}
}
