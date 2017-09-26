using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Brains myBrain;
	public TweakablePlayerValues myValues;
	private Rigidbody2D myRB;
	private int playerState = 0; //0 is ground, 1 is aerial, 2 is wall, 3 is roll

	// Use this for initialization
	void Start () {
		myRB = this.GetComponent<Rigidbody2D>();
		myBrain.Initialize(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//called every physics update
	void FixedUpdate (){
		myBrain.RunBrain();
		myRB.AddForce(-myRB.velocity.normalized * (myValues.quadDrag * myRB.velocity.sqrMagnitude));
	}

	void GroundState(){

	}

	void AerialState(){

	}

	void WallState(){

	}

	void RollState(){

	}

	void Jump (){

	}

	public void GroundMove(float moveInput){ //-1 to 1 from horizontal axis
		
		myRB.AddForce(Vector2.right * moveInput * myValues.groundAccel);
	}

	void AerialMove(){

	}

	void Roll(){

	}

	void WallSlide(){

	}

	void WallJump(){

	}
}
