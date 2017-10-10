using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoCanvasManager : MonoBehaviour {

	public GameObject Player1;
	private Player player1Stuff;
	public GameObject Player2;
	private Player player2Stuff;

	public Text GameStatusText;
	public Text Player1Stats;
	public Text Player2Stats;

	// Use this for initialization
	void Start () {
		player1Stuff = Player1.GetComponent<Player>();
		player2Stuff = Player2.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Player1 != null && Player2 != null){
			//display stats and fight!
			GameStatusText.text = "Fight!";
			Player1Stats.text = player1Stuff.AttackFunction.itemName + " " + player1Stuff.RollFunction.itemName + " " + player1Stuff.JumpFunction.itemName + " " + player1Stuff.AirFunction.itemName;
			Player2Stats.text = player2Stuff.AttackFunction.itemName + " " + player2Stuff.RollFunction.itemName + " " + player2Stuff.JumpFunction.itemName + " " + player2Stuff.AirFunction.itemName;
		}
		else if (Player1 == null){
			//2 wins
			GameStatusText.text = "P2 Wins! Press U to Restart";
		}
		else {
			//1 wins
			GameStatusText.text = "P1 Wins! Press U to Restart";
		}
	}
}
