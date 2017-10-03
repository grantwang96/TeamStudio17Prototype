using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKillPlayer : MonoBehaviour { //goes on something with a hitbox

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag != this.tag) {
			Destroy (other.gameObject);
		}
	}
}
