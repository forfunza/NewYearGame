using UnityEngine;
using System.Collections;

public class supply : MonoBehaviour {

	public GameObject target1;

	// Use this for initialization
	void Start () {
		//InvokeRepeating("cloneTarget",1, 0.7F );
	}
	
	// Update is called once per frame
	void Update () {



	}

	void cloneTarget(){
		GameObject clone_enemy;
		clone_enemy = Instantiate (target1, transform.position, transform.rotation) as GameObject;

	}
}
