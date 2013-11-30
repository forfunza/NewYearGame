using UnityEngine;
using System.Collections;

public class LockScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DisableMouseLook(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void DisableMouseLook(bool enable)
	{
		GameObject FPC= GameObject.FindWithTag("Player");
		
		FPC.transform.GetComponent<CharacterMotor>().enabled = enable;
		FPC.transform.GetComponent<MouseLook>().enabled = enable;
		Camera.mainCamera.GetComponent<MouseLook>().enabled = enable;
		
	}
}
