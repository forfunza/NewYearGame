using UnityEngine;
using System.Collections;

public class target : MonoBehaviour {

	public static float smooth = 3.0F;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	void Update() {
		transform.Translate(Vector3.left * smooth * Time.deltaTime);
	}
}
