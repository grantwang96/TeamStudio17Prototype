using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartsHere : MonoBehaviour {

	public LevelGenerator thisLevel;
	public bool doneIS = false;
	public bool playerMake = false;
	public Transform playerPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(thisLevel.done && !doneIS && !playerMake){
			
			doneIS = true;
			Instantiate (playerPrefab, thisLevel.playerStartPos [0], Quaternion.identity);
			playerMake = true;

		}
	}

	void IsLevelDone(){
		doneIS = thisLevel.done;
	}
}
