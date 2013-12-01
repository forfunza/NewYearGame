using UnityEngine;
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
					Destroy (Shot.transform.gameObject,2);
				}
			}else{
				if(gameObject.tag == "bullet"){
					Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
					PhotonNetwork.Destroy (gameObject);
					Destroy (Shot.transform.gameObject,2);
				}
			}

		}else if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2") {
			if(PhotonNetwork.isMasterClient){
				if(other.gameObject.tag == "Player2"){
					print("222222222222222");
					Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
					PhotonNetwork.Destroy (gameObject);
					Destroy (Shot.transform.gameObject,2);
				}
			}else{
				if(other.gameObject.tag == "Player"){
					print("1111111111111111");
					Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
					PhotonNetwork.Destroy (gameObject);
					Destroy (Shot.transform.gameObject,2);
				}
			}
		}
		
	}
}
