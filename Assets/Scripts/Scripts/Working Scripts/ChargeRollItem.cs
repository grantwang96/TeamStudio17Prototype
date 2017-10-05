using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateReplacingItem/ChargeRollItem")]
public class ChargeRollItem : StateReplacingItem {

	public float chargeRate;
	public float chargeToDurationRatio;
	public float chargeToSpeedRatio;
	private float rollDuration;
	private float rollSpeed;

	public override void RunFunction (Player caller)
	{
		if (!caller.rolling) {
			caller.rollTimer = 0;
			caller.rolling = true;
			if (caller.facing > 0) {
				caller.velocity = new Vector2 (rollSpeed, 0);
			} else {
				caller.velocity = new Vector2 (-rollSpeed, 0);
			}
		}
		if (!caller.grounded) {
			caller.rollTimer = 0;
			caller.rolling = false;
		}
		else if (caller.rollTimer >= rollDuration) {
			caller.rollTimer = 0;
			caller.rolling = false;
			caller.velocity = Vector2.zero;
			caller.rollQueued = false;
		}
	}
}