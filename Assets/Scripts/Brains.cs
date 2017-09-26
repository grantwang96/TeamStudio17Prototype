using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Brains : ScriptableObject {
	protected Player myPlayer;

	public virtual void RunBrain(){
		//base functions
	}

	public void Initialize (Player owner) {
		myPlayer = owner;
	}
	//to be continued
}

[CreateAssetMenu(fileName = "Brains/KeyBoardBrain")]
public class KeyboardBrain : Brains {
	public override void RunBrain ()
	{
		float xInput = Input.GetAxis("Horizontal");
		if (xInput != 0) myPlayer.GroundMove (xInput);
	}

	
}
