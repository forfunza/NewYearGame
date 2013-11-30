using UnityEngine;
using System.Collections;

public class CheckTrack : MonoBehaviour
{
	public GameObject paticle;
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
		if (other.tag == "wall") {
			GameObject Shot;
			Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
			PhotonNetwork.Destroy (gameObject);
			Destroy (Shot.transform.gameObject,2);
		}else if(other.tag == "bullet"){
			GameObject Shot;
			Shot = Instantiate (paticle,transform.position,transform.rotation) as GameObject;
			PhotonNetwork.Destroy (gameObject);
			Destroy (Shot.transform.gameObject,2);
		}
		
	}
}
