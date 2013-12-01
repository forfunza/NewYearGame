using UnityEngine;
using System.Collections;

public class item : Photon.MonoBehaviour {



	public GameObject itemBox;
	// Use this for initialization
	void Start () {
		if(PhotonNetwork.isMasterClient){
			InvokeRepeating("randomItem", 0.7F ,Random.Range(30, 50) );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void randomItem(){
		GameObject cloning;
		PhotonNetwork.Instantiate ("Giftbox", new Vector3(Random.Range(-7.50F, 4.0F),7,transform.position.z), transform.rotation,0) ;
		
	}


}
