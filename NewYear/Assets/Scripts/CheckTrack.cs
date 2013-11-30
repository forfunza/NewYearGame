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
				if (other.tag == "bullet") {
						GameObject Shot;
						Shot = Instantiate (paticle, other.transform.position, other.transform.rotation) as GameObject;
						Destroy (other.gameObject);
						Destroy (Shot.transform.gameObject, 2);
				}

		}
}
