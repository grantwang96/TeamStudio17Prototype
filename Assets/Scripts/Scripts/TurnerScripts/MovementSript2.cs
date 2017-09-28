using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSript2 : MonoBehaviour {


	public float movementspeed = 1.0f;
	public float jumpForce = 10f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * movementspeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * movementspeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.up * movementspeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.down * movementspeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.Space)){
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpForce));
		}
	}


}
