﻿using UnityEngine;
using System.Collections;

public class CheckTrack : Photon.MonoBehaviour
{
	public GameObject paticle;
	private string name;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	void OnTriggerEnter (Collider other)
	{
		GameObject Shot;
		if (other.gameObject.tag == "wall") {
			Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
			PhotonNetwork.Destroy (gameObject);
			Destroy (Shot.transform.gameObject,2);
		}else if(other.gameObject.tag == "bullet2" || other.gameObject.tag == "bullet"){
			if(PhotonNetwork.isMasterClient){
				if(gameObject.tag == "bullet2"){
					Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
					PhotonNetwork.Destroy (gameObject);
					Destroy (Shot.transform.gameObject,1);
				}
			}else{
				if(gameObject.tag == "bullet"){
					Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
					PhotonNetwork.Destroy (gameObject);
					Destroy (Shot.transform.gameObject,1);
				}
			}

		}else if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2") {
			if(PhotonNetwork.isMasterClient){
				if(other.gameObject.tag == "Player2"){
					Shot = Instantiate (paticle,new Vector3(transform.position.x,transform.position.y,transform.position.z-8),transform.rotation) as GameObject;
					PhotonNetwork.Destroy (gameObject);
					Destroy (Shot.transform.gameObject,1);

//					photonView.RPC("setHpPlayer",PhotonTargets.All,4);
//					setHpPlay(4);
				}
			}else{
				if(other.gameObject.tag == "Player"){
					Shot = Instantiate (paticle,new Vector3(transform.position.x,transform.position.y,transform.position.z-8),transform.rotation) as GameObject;
					PhotonNetwork.Destroy (gameObject);
					Destroy (Shot.transform.gameObject,1);
//					photonView.RPC("setHpPlayer",PhotonTargets.All,10);
//					setHpPlay(10);
				}
			}
		}
		
	}

//	[RPC]
//	public void setHpPlayer (int hp)
//	{
//		print("In It");
//		GameEvent ge =  gameObject.GetComponent<GameEvent>();
//		EnergyBar eg = ge.hpbar2.GetComponent<EnergyBar> ();
//		eg.SetValueCurrent(hp);
//
//		//	hpbar.SetValueCurrent(hp);
//	}

}
