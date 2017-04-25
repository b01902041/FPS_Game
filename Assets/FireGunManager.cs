using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGunManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other) {
		//Rigidbody body = other.GetComponent<Rigidbody>();
		Debug.Log("collision");
		other.gameObject.SendMessage ("Hit", 5);

	}


}
