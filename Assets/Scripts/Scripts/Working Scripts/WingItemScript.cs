using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionReplacingItem/WingsItem")]
public class WingItemScript : ActionReplacingItem {

	public float fromGroundJumpforce;
	public float aerialFlapForce;

	public override void RunFunction (Player caller) {

		if (caller.grounded) {
			caller.velocity += Vector2.up * fromGroundJumpforce;
		}
		else {
			caller.velocity += Vector2.up * aerialFlapForce;
		}
	}
}
