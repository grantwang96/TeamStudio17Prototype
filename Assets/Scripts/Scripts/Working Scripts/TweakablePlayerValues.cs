using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "TweakablePVals")]
public class TweakablePlayerValues : ScriptableObject {

	public float jumpForce;

	public float groundAccel;

	public float aerialAccel;

	public float wallSlideSpeed; //starts once vertical velocity is below that speed

	public float rollSpeed;

	public float quadDragG;

	public float quadDragA;
}
