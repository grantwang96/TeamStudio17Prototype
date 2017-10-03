using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

	public string itemName;
	public string itemDescription;
	public int itemValue;
	public int itemType; //0 is state replacing, 1 is action, 2 is bonus

	public virtual void RunFunction(Player caller){

	}

}

public class StateReplacingItem : Item {
	
	public void Initialize (){
		itemType = 0;
	}
}
	

public class ActionReplacingItem : Item {

	public void Initialize (){
		itemType = 1;
	}
}
	
public class BonusItem : Item {

	public void Initialize (){
		itemType = 2;
	}

}

//Default stateReplacers
[CreateAssetMenu(menuName = "StateReplacingItem/DefaultGround")]
public class DefaultGroundItem : StateReplacingItem {

	public float speed;
	public override void RunFunction (Player caller)
	{
		caller.velocity = new Vector2 (caller.horizontalInput * speed, 0);
	}
}

[CreateAssetMenu(menuName = "StateReplacingItem/DefaultAir")]
public class DefaultAirItem : StateReplacingItem {

	public float acceleration;
	public float gravity;
	public float bonusAntiJumpGrav;
	public override void RunFunction (Player caller)
	{
		caller.velocity += new Vector2 (caller.horizontalInput * acceleration, -gravity);
		if (caller.velocity.y > 0 && !caller.jumpHeld)
			caller.velocity += Vector2.down * bonusAntiJumpGrav;
	}
}

[CreateAssetMenu(menuName = "StateReplacingItem/DefaultWall")]
public class DefaultWallItem : StateReplacingItem {

	public float slideSpeed;
	public float driftOffSpeed;
	public override void RunFunction (Player caller)
	{
		caller.velocity = new Vector2 (0, -slideSpeed);
		if (caller.horizontalInput < 0 && caller.facing < 0) {
			caller.velocity += Vector2.right * caller.horizontalInput * driftOffSpeed; 
		} else if (caller.horizontalInput > 0 && caller.facing > 0) {
			caller.velocity += Vector2.right * caller.horizontalInput * driftOffSpeed; 
		}
	}
}

[CreateAssetMenu(menuName = "StateReplacingItem/DefaultRoll")]
public class DefaultRollItem : StateReplacingItem {

	public float rollDuration;
	public float rollInitialBonus; //decays over time
	public float rollSpeed;
	public override void RunFunction (Player caller)
	{
		if (!caller.rolling) {
			caller.rollTimer = 0;
			caller.rolling = true;
			if (caller.facing > 0) {
				caller.velocity = new Vector2 (rollSpeed + (rollInitialBonus * (rollDuration - caller.rollTimer / rollDuration)), 0);
			} else {
				caller.velocity = new Vector2 (-rollSpeed - (rollInitialBonus * (rollDuration - caller.rollTimer / rollDuration)), 0);
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
		}
	}
}

[CreateAssetMenu(menuName = "ActionReplacingItem/DefaultAttack")]
public class DefaultAttackItem : ActionReplacingItem {

	public float attackDuration;
	public Vector2 attackOffset;
	public GameObject hitBoxPrefab;

	public override void RunFunction (Player caller)
	{
		DoAttack (attackDuration, caller);
	}

	IEnumerator DoAttack(float duration, Player aggressor){
		float internalTimer = 0;
		GameObject attackBox;
		attackBox = (GameObject)Instantiate (hitBoxPrefab, aggressor.transform.position + new Vector3 (attackOffset.x * aggressor.facing, attackOffset.y), Quaternion.identity);
		attackBox.transform.parent = aggressor.transform;
		attackBox.tag = aggressor.tag;
		attackBox.name = aggressor.gameObject.name + "'s hitbox";
		while (internalTimer < duration) {

			internalTimer += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		DestroyImmediate (attackBox);
	}
}

[CreateAssetMenu(menuName = "ActionReplacingItem/DefaultJump")]
public class DefaultJumpItem : ActionReplacingItem {

	public float jumpForce;

	public override void RunFunction (Player caller)
	{
		if (caller.grounded) {
			caller.velocity += Vector2.up * jumpForce;
		}
	}
}

[CreateAssetMenu(menuName = "ActionReplacingItem/DefaultWallJump")]
public class DefaultWallJump : ActionReplacingItem {
	public float verticalJumpForce;
	public float horizontalJumpForce;

	public override void RunFunction (Player caller)
	{
		if (caller.onWall) {
			caller.velocity += new Vector2 (caller.facing * horizontalJumpForce, verticalJumpForce);
		}
	}
}
