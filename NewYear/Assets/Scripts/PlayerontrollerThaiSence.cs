using UnityEngine;
using System.Collections;

public class PlayerontrollerThaiSence : Photon.MonoBehaviour
{
		public GameObject Bullet;
		public GameObject Bullet2;
		// Use this for initialization
		void Start ()
		{
				if (!photonView.isMine) {
						PlayerontrollerThaiSence fps = gameObject.GetComponent<PlayerontrollerThaiSence> ();
						MouseLook mouseLook = gameObject.GetComponent<MouseLook> ();
						GameObject cameraObject = transform.FindChild ("Main Camera").gameObject;
						Destroy (fps);
						Destroy (mouseLook);
						cameraObject.SetActive (false);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (PhotonNetwork.isMasterClient) {
				if (Input.GetKey ("a") || Input.GetKey ("left")) {
						transform.position = new Vector3 (transform.position.x - 0.3f, transform.position.y, transform.position.z);
						if (transform.position.x < -9f) {
								transform.position = new Vector3 (-9f, transform.position.y, transform.position.z);
						}
				}
				if (Input.GetKey ("d") || Input.GetKey ("right")) {
						transform.position = new Vector3 (transform.position.x + 0.3f, transform.position.y, transform.position.z);
						if (transform.position.x > 5.3f) {
								transform.position = new Vector3 (5.3f, transform.position.y, transform.position.z);
						}
				}
				if (Input.GetKeyUp ("space")) {	
						//InstantiateBullet (transform.position + transform.forward * 2, transform.rotation, transform.TransformDirection (Vector3.forward * 40));
						GameObject clone_enemy;
				print("Position : "+transform.position.x+" "+transform.position.y+" "+transform.position.z);
				clone_enemy = PhotonNetwork.Instantiate ("Bullet", new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation,0);
				clone_enemy.rigidbody.velocity = transform.forward * 15.0f;
			}
			}else{
				if (Input.GetKey ("a") || Input.GetKey ("left")) {
				transform.position = new Vector3 (transform.position.x + 0.3f, transform.position.y, transform.position.z);
				if (transform.position.x > 5.3f) {
					transform.position = new Vector3 (5.3f, transform.position.y, transform.position.z);
				}
			}
			if (Input.GetKey ("d") || Input.GetKey ("right")) {

				transform.position = new Vector3 (transform.position.x - 0.3f, transform.position.y, transform.position.z);
				if (transform.position.x < -9f) {
					transform.position = new Vector3 (-9f, transform.position.y, transform.position.z);
				}
			}
			if (Input.GetKeyUp ("space")) {	
					//InstantiateBullet (transform.position + transform.forward * 2, transform.rotation, transform.TransformDirection (Vector3.forward * 40));
					GameObject clone_enemy;
				clone_enemy = PhotonNetwork.Instantiate ("Bullet2", new Vector3 (transform.position.x, transform.position.y, transform.position.z),transform.rotation,0);
				clone_enemy.rigidbody.velocity = transform.forward * 15.0f;
			}
		}
	}
	
	//	void InstantiateBullet (Vector3 srcPosition, Quaternion srcRotation, Vector3 srcVelocity)
//	{
//		GameObject arrow = PhotonNetwork.Instantiate ("Prefabs/Weapons/Arrow", srcPosition, srcRotation, 0);
//		arrow.rigidbody.velocity = srcVelocity;
//		Destroy (arrow, 10);
//	}
}
