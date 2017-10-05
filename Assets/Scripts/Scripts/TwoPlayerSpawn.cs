using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerSpawn : MonoBehaviour {

    public Transform player1Prefab;
    public Transform player2Prefab;
    public LevelGenerator levelGen;
    bool spawned = false;

    public CameraCtrl cameraControlScript;
    public List<ActionReplacingItem> attacks = new List<ActionReplacingItem>();
    public List<ActionReplacingItem> jumps = new List<ActionReplacingItem>();
    public List<StateReplacingItem> rolls = new List<StateReplacingItem>();

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
        modifyLoadout(Random.Range(0, 3), newPlayer1.gameObject);
        modifyLoadout(Random.Range(0, 3), newPlayer2.gameObject);
        cameraControlScript.gameObject.SetActive(true);
        cameraControlScript.p1 = newPlayer1.gameObject;
        cameraControlScript.p2 = newPlayer2.gameObject;
        cameraControlScript.p1cam = newPlayer1.Find("Camera").gameObject;
        cameraControlScript.p2cam = newPlayer2.Find("Camera").gameObject;
        spawned = true;
    }

    void modifyLoadout(int val, GameObject player) {
        if(val > 0)
        {
            player.GetComponent<Player>().JumpFunction = jumps[Random.Range(0, jumps.Count)];
        }
        if(val > 1)
        {
            player.GetComponent<Player>().AttackFunction = attacks[Random.Range(0, attacks.Count)];
        }
        if(val > 2)
        {
            player.GetComponent<Player>().RollFunction = rolls[Random.Range(0, rolls.Count)];
        }
    }
}
