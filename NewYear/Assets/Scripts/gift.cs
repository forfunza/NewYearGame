using UnityEngine;
using System.Collections;

public class gift : Photon.MonoBehaviour {
	

	void OnTriggerEnter(Collider other) {


		if(other.tag == "land"){

			PhotonNetwork.Destroy(gameObject);
			
		}
	}
}
