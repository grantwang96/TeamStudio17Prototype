using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateReplacingItem/ChargeRollItem")]
public class ChargeRollItem : StateReplacingItem {

	public float chargeRate;
	public float chargeToDurationRatio;
	public float chargeToSpeedRatio;
	public float maxDur;
	public float maxSpeed;
	private float rollDuration;
	private float rollSpeed;

	private bool rollStarted = false;

	public override void RunFunction (Player caller)
	{
		if (!caller.rolling) {
			rollDuration = 0;
			rollSpeed = 0;
			caller.rolling = true;
			caller.rollTimer = 0;
//			if (caller.facing > 0) {
//				caller.velocity = new Vector2 (rollSpeed, 0);
//			} else {
//				caller.velocity = new Vector2 (-rollSpeed, 0);
//			}
		}
		if (caller.rolling && caller.rollQueued && !rollStarted) {
			caller.rollTimer = 0;
			rollDuration += Time.deltaTime * chargeToDurationRatio;
			rollSpeed += Time.deltaTime * chargeToSpeedRatio;
			if (rollDuration > maxDur) {
				rollDuration = maxDur;
			}
			if (rollSpeed > maxSpeed) {
				rollSpeed = maxSpeed;
			}
		}
		if (!caller.rollQueued && !rollStarted) {
			rollStarted = true;
			if (caller.facing > 0) {
				
				caller.velocity = new Vector2 (rollSpeed, 0);

			} else {
				
				caller.velocity = new Vector2 (-rollSpeed, 0);
			}
		}
		if (!caller.grounded) {
			caller.rollTimer = 0;
			caller.rolling = false;
			rollStarted = false;
		}

		else if (caller.rollTimer > rollDuration) {
			caller.rollTimer = 0;
			caller.rolling = false;
			caller.velocity = Vector2.zero;
			caller.rollQueued = false;
			rollStarted = false;
		}
	}
}