using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {



	public GameObject itemBox;
	// Use this for initialization
	void Start () {
		InvokeRepeating("randomItem", 0.7F ,Random.Range(10, 20) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void randomItem(){
		GameObject cloning;
		cloning = Instantiate (itemBox, new Vector3(Random.Range(-3.19F, 3.19F),transform.position.y,transform.position.z), transform.rotation) as GameObject;
		
	}
}
