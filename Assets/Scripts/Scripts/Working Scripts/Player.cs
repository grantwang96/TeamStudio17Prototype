using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public Brains myBrain;
	public TweakablePlayerValues myValues;
	public CheckerScript FloorCheck;
	public CheckerScript RightWallCheck;
	public CheckerScript LeftWallCheck;
	public StateReplacingItem GroundFunction;
	public StateReplacingItem AirFunction;
	public StateReplacingItem WallFunction;
	public StateReplacingItem RollFunction;
	public ActionReplacingItem AttackFunction;
	public ActionReplacingItem JumpFunction;
	public ActionReplacingItem WallJumpFunction;

	public List<BonusItem> extraItems;
	private Rigidbody2D myRB;
	public Transform myCursor;

	public bool jumpQueued;
	public bool jumpHeld;
	public bool grounded;
	public bool attackQueued;
	public float attackTimer;
	public bool rollQueued;
	public bool rolling;
	public float rollTimer;
	public bool onWall;
	public float horizontalInput;
	public int facing = 1;

	public Vector2 velocity;

	// Use this for initialization
	void Start () {
		myRB = this.GetComponent<Rigidbody2D>();
		myBrain.Initialize(this);
		velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		myBrain.RunBrain();
		if (attackTimer > 0) attackTimer -= Time.deltaTime;

		if (facing > 0){
			myCursor.localPosition = new Vector3(.25f, 0f, 0f);
		}

		else if (facing < 0){
			myCursor.localPosition = new Vector3(-.25f, 0f, 0f);
		}
	}

	//called every physics update
	void FixedUpdate (){
		

		StateManager ();

		if (jumpQueued) {
			if (!onWall) {
				JumpFunction.RunFunction (this);
			} else {
				WallJumpFunction.RunFunction (this);
			}
		}

		if (attackQueued) {
			AttackFunction.RunFunction (this);
		}

		myRB.MovePosition (myRB.position + (velocity * Time.fixedDeltaTime));
		//quadDrag ();
	}

	void StateManager(){
		if (FloorCheck.positiveCheck) {
			grounded = true;
		} else {
			grounded = false;
			if ((RightWallCheck.positiveCheck || LeftWallCheck.positiveCheck)) {
				onWall = true;
				velocity = new Vector2 (0, velocity.y);
				if (RightWallCheck.positiveCheck)
					facing = -1;
				else if (LeftWallCheck.positiveCheck)
					facing = 1;
			} else {
				onWall = false;
			}
		}
		if (rollQueued || rolling) {
			RollFunction.RunFunction (this);
			rollTimer += Time.fixedDeltaTime;
		}
		else if (grounded) {
			// else {
				GroundFunction.RunFunction (this);
			//}
		} else if (onWall && velocity.y < 0) {
			WallFunction.RunFunction (this);
		} 
		else {
			AirFunction.RunFunction (this);
		}
		if (velocity.x > 0)
			facing = 1;
		else if (velocity.x < 0)
			facing = -1;
	}

//	void quadDrag(){
//		if (playerState == 0){
//			myRB.AddForce(-myRB.velocity.normalized * (myValues.quadDragG * myRB.velocity.sqrMagnitude));
//		}
//		else if (playerState == 1) {
//			myRB.AddForce (-myRB.velocity.normalized * (myValues.quadDragA * myRB.velocity.sqrMagnitude));
//		}
//	}
//
//	void OnCollisionEnter2D(Collision2D col){
//		if (col.collider.tag == "Ground") {
//			grounded = true;
//		} else if (col.collider.tag == "Wall") {
//			onWall = true;
//			touchedWallX = col.transform.position.x;
//		}
//	}
//
//	void OnCollisionExit2D(Collision2D col){
//		if (col.collider.tag == "Ground") {
//			grounded = false;
//		} else if (col.collider.tag == "Wall") {
//			onWall = false;
//		}
//	}
}
