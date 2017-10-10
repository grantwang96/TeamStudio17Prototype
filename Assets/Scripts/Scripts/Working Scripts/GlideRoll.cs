using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateReplacingItem/GlideRoll")]
public class GlideRoll : StateReplacingItem {

	private float rollDuration = 1;
	public float rollSpeed;
	public float inAirFallSpeed;
	public override void RunFunction (Player caller)
	{
		if (caller.grounded) { //cannot glide on ground
			caller.rollTimer = 0;
			caller.rolling = false;
			caller.rollQueued = false;
		} else if (!caller.rolling) {
			caller.rollTimer = 0;
			caller.rolling = true;
			if (caller.facing > 0) {
				caller.velocity = new Vector2 (rollSpeed, -inAirFallSpeed);
			} else {
				caller.velocity = new Vector2 (-rollSpeed, -inAirFallSpeed);
			}
		} else if (caller.rolling) {
			caller.rollTimer = 0;
			if (caller.rollQueued = false || caller.jumpQueued) {
				caller.rolling = false;
				caller.rollTimer = 0;
				caller.rolling = false;
				caller.rollQueued = false;
			}
		}
	}
}
