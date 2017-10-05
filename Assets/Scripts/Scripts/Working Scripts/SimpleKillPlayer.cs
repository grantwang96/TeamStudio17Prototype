using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKillPlayer : MonoBehaviour { //goes on something with a hitbox

	public float lifetime;
	private float measuredLife = 0f;

	void Update(){
		measuredLife += Time.deltaTime;
		if (measuredLife >= lifetime) DestroyImmediate(this.gameObject);
	}

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag != this.tag && other.gameObject.name.Contains("Player")) {
			Destroy (other.gameObject);
		}
	}
}
