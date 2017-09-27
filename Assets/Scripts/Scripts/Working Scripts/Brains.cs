using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need :
//cursor pointer script
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

[CreateAssetMenu(fileName = "KeyBoardBrain")]
public class KeyboardBrain : Brains {
	public override void RunBrain ()
	{
		myPlayer.horizontalInput = Input.GetAxis ("Horizontal");

//		float xInput = Input.GetAxis("Horizontal");
//		if (xInput != 0) myPlayer.GroundMove (xInput);
		if (Input.GetKeyDown (KeyCode.Space)){ 
			myPlayer.qJump = true;
		}





	}

	
}

[CreateAssetMenu(fileName = "ControllerBrain")]
public class ControllerBrain : Brains {
	public override void RunBrain ()
	{
		myPlayer.horizontalInput = Input.GetAxis ("Horizontal");

		//		float xInput = Input.GetAxis("Horizontal");
		//		if (xInput != 0) myPlayer.GroundMove (xInput);
		if (Input.GetButtonDown ("Fire2")){ 
			myPlayer.qJump = true;
		}





	}


}
