using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerScript : MonoBehaviour {

	public bool positiveCheck = false;
	public string tagToCheckFor;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == tagToCheckFor) {
			positiveCheck = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == tagToCheckFor) {
			positiveCheck = false;
		}
	}
}
