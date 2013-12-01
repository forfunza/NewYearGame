using UnityEngine;
using System.Collections;

public class headAction : MonoBehaviour
{
		private bool isRotateRight = false;
		private bool isRotateLeft = false;
		private bool isFirstRotate = true;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!PhotonNetwork.isMasterClient) {
						if (Input.GetKey ("a") || Input.GetKey ("left")) {
								if (isRotateRight == false) {
										if (isFirstRotate == true) {
												transform.Rotate (new Vector3 (0, 0, 10f));
												isRotateRight = true;
												isRotateLeft = false;
										} else {
												transform.Rotate (new Vector3 (0, 0, 20f));
												isRotateRight = true;
												isRotateLeft = false;
										}
								}

						}
						if (Input.GetKey ("d") || Input.GetKey ("right")) {
								if (isRotateLeft == false) {
										if (isFirstRotate == true) {
												transform.Rotate (new Vector3 (0, 0, -10f));
												isRotateRight = false;
												isRotateLeft = true;
										} else {
												transform.Rotate (new Vector3 (0, 0, -20f));
												isRotateRight = false;
												isRotateLeft = true;
										}
								}
						
						}
				}
		}
}
