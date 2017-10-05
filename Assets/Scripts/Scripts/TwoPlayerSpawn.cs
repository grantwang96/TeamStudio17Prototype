using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerSpawn : MonoBehaviour {

    public Transform player1Prefab;
    public Transform player2Prefab;
    public LevelGenerator levelGen;
    bool spawned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(levelGen.done && !spawned)
        {
            spawnPlayers();
        }
	}

    void spawnPlayers()
    {
        Transform newPlayer1 = Instantiate(player1Prefab, levelGen.playerStartPos[0], Quaternion.identity);
        Transform newPlayer2 = Instantiate(player2Prefab, levelGen.playerStartPos[1], Quaternion.identity);
        spawned = true;
    }
}
