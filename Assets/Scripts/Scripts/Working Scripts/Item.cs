using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

	public Player owner;
	public bool isCoolingDown;
	public float coolDownTime;
	protected float currentCooldown;

	// Use this for initialization
	public virtual void Initialize(Player caller){
		owner = caller;
	}

	public virtual void Execute(){ //run .base function at the end of the override
		//something
		isCoolingDown = true;
	}

	public virtual void Cooldown(){ //call in update
		if (isCoolingDown) {
			currentCooldown += Time.deltaTime;
		}
		if (currentCooldown >= coolDownTime) {
			currentCooldown = 0;
			isCoolingDown = false;
		}
	}
}

public class GrapplingHook : Item {

	public override void Execute ()
	{
		if (!isCoolingDown) {

		}
		base.Execute ();
	}




}
