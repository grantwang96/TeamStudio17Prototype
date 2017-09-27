using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Brains myBrain;
	public TweakablePlayerValues myValues;
	private Rigidbody2D myRB;

	private int playerState = 0; //0 is ground, 1 is aerial, 2 is wall, 3 is roll
	public bool qJump;
	public float horizontalInput;

	// Use this for initialization
	void Start () {
		myRB = this.GetComponent<Rigidbody2D>();
		myBrain.Initialize(this);
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("qJUMP:" + qJump);
//		Debug.Log ("playerState" + playerState);
//		Debug.Log ("Grounded" + isGrounded () + " " + Time.realtimeSinceStartup); 
//		Debug.Log(myRB.velocity);
	}

	//called every physics update
	void FixedUpdate (){
		myBrain.RunBrain();

		if (playerState == 0){
			GroundState ();
		}
		if (playerState == 1){
			qJump = false; //bandaid to be fixed later (the boolean sets to true while in air if you click space bar... causes it to "bounce"
			AerialState ();
		}
		if (playerState == 2){
			WallState ();
		}
		if (playerState == 3){
			RollState ();
		}
		quadDrag ();
		stateController ();
	}

	void stateController(){
		if (isGrounded()){
			playerState = 0;
		}
		else if (!isGrounded()){
			playerState = 1;
		}
	}

	void GroundState(){
		GroundMove ();
		if (qJump == true){
			if(isGrounded()){
				playerState = 1;
				Jump ();
			}
			qJump = false;
		}

	}

	void AerialState(){
		AerialMove ();
		Debug.Log("movement");
	}

	void WallState(){

	}

	void RollState(){

	}

	void Jump (){
		myRB.AddForce (Vector2.up * myValues.jumpForce, ForceMode2D.Impulse); 
	}

	public void GroundMove(){ //-1 to 1 from horizontal axis
		
		myRB.AddForce(Vector2.right * horizontalInput * myValues.groundAccel);
		if (horizontalInput == 0){
			cancelXVelocity ();
		}
	}

	void AerialMove(){
		myRB.AddForce(Vector2.right * horizontalInput * myValues.aerialAccel);
	}

	void Roll(){

	}

	void WallSlide(){

	}

	void WallJump(){

	}

	void quadDrag(){
		if (playerState == 0){
			myRB.AddForce(-myRB.velocity.normalized * (myValues.quadDragG * myRB.velocity.sqrMagnitude));
		}
		else if (playerState == 1) {
			myRB.AddForce (-myRB.velocity.normalized * (myValues.quadDragA * myRB.velocity.sqrMagnitude));
		}
	}

	bool isGrounded(){
		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, Vector2.down, (transform.localScale.y * 0.6f), (1 << LayerMask.NameToLayer ("Ground")));
		if(hit.collider != null) {
//			playerState = 0;
			return true;
		} else {
			return false;
		}
//		if (Physics2D.Raycast (this.transform.position, Vector2.down, 1.0f, LayerMask.NameToLayer ("Ground"))){
//			return true;
//		}
//		else{return false;}
	}

	void cancelXVelocity(){
		myRB.velocity = new Vector2 (0, myRB.velocity.y);
	}

}
