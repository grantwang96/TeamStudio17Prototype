using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateReplacingItem/JetpackAerialItem")]
public class JetpackAerialItem : StateReplacingItem {

	public float acceleration;
	public float drag;
	public float gravity;
	public float bonusAntiJumpGrav;
	public float jetPackForce;
	public override void RunFunction (Player caller)
	{
		caller.velocity += new Vector2 (caller.horizontalInput * acceleration, -gravity);
		if (caller.velocity.y > 0 && !caller.jumpHeld)
			caller.velocity += Vector2.down * bonusAntiJumpGrav;
		else if (caller.jumpHeld) {
			caller.velocity += Vector2.up * jetPackForce;
		}
		caller.velocity += -caller.velocity.normalized * (drag * caller.velocity.sqrMagnitude);
	}
}
