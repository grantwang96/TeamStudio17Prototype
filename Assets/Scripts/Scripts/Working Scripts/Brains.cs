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

[CreateAssetMenu(fileName = "WASDKeyBoardBrain")]
public class WASDKeyboardBrain : Brains {
	public override void RunBrain ()
	{
		myPlayer.horizontalInput = Input.GetAxisRaw ("WASDHorizontal");

//		float xInput = Input.GetAxis("Horizontal");
//		if (xInput != 0) myPlayer.GroundMove (xInput);
		if (Input.GetKeyDown (KeyCode.W)){ 
			myPlayer.jumpQueued = true;
		}
		else {
			myPlayer.jumpQueued = false;
		}
		if (Input.GetKey (KeyCode.W)) { 
			myPlayer.jumpHeld = true;
		} else {
			myPlayer.jumpHeld = false;
		}
		if (Input.GetKeyDown(KeyCode.G) && !myPlayer.attackQueued) {
			myPlayer.attackQueued = true;
		}
		else {
			myPlayer.attackQueued = false;
		}
		if (Input.GetKeyDown(KeyCode.F)){
			myPlayer.rollQueued = true;
		}
		//Vector3 tempV3 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//myPlayer.myCursor.position = new Vector3(tempV3.x, tempV3.y);
	}
}

[CreateAssetMenu(fileName = "ARROWSKeyBoardBrain")]
public class ARROWSKeyboardBrain : Brains {
	public override void RunBrain ()
	{
		myPlayer.horizontalInput = Input.GetAxisRaw ("ARROWHorizontal");

		//		float xInput = Input.GetAxis("Horizontal");
		//		if (xInput != 0) myPlayer.GroundMove (xInput);
		if (Input.GetKeyDown (KeyCode.UpArrow)){ 
			myPlayer.jumpQueued = true;
		}
		else {
			myPlayer.jumpQueued = false;
		}
		if (Input.GetKey (KeyCode.UpArrow)) { 
			myPlayer.jumpHeld = true;
		} else {
			myPlayer.jumpHeld = false;
		}
		if (Input.GetKeyDown(KeyCode.Slash) && !myPlayer.attackQueued) {
			myPlayer.attackQueued = true;
		}
		else {
			myPlayer.attackQueued = false;
		}
		if (Input.GetKeyDown(KeyCode.Period)){
			myPlayer.rollQueued = true;
		}
		//Vector3 tempV3 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//myPlayer.myCursor.position = new Vector3(tempV3.x, tempV3.y);
	}
}

[CreateAssetMenu(fileName = "ControllerBrain")]
public class ControllerBrain : Brains {
	public int controllerID = 1;

	public override void RunBrain ()
	{
		myPlayer.horizontalInput = Input.GetAxis ("Horizontal" + controllerID);

		//		float xInput = Input.GetAxis("Horizontal");
		//		if (xInput != 0) myPlayer.GroundMove (xInput);
		if (Input.GetButtonDown ("Fire2")) { 	
			Debug.Log("buttonpressed");
			myPlayer.jumpQueued = true;
		} else {
			myPlayer.jumpQueued = false;
		}
		if (Input.GetButton ("Fire2")) { 
			myPlayer.jumpHeld = true;
		} else {
			myPlayer.jumpHeld = false;
		}
		if (Input.GetButtonDown("Fire1")){
			myPlayer.attackQueued = true;
		}
		else {
			myPlayer.attackQueued = false;
		}
	}
}
