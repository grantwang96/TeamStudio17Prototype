using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag != this.tag && other.name.Contains("Player")) {
			Destroy (other.gameObject);
		}
		Debug.Log("SHIT");
		Destroy(this.gameObject);
	}
}
