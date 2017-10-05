using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionReplacingItem/DoubleJump")]
public class DoubleJumpItem : ActionReplacingItem {

	public float jump1Force;
	public float jump2Force;
	private bool doubleJumped = false;

	public override void RunFunction (Player caller) {
		
		if (caller.grounded) {
			doubleJumped = false;
			caller.velocity += Vector2.up * jump1Force;
		}
		else if (!doubleJumped){
			doubleJumped = true;
			caller.velocity += Vector2.up * jump2Force;
		}
	}
}
