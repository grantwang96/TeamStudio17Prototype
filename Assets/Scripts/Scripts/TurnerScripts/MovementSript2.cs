using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSript2 : MonoBehaviour {


	public float movementspeed = 1.0f;
	public float jumpForce = 10f;

	public GameObject pickaxe;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * movementspeed * Time.deltaTime;
			pickaxe.transform.localPosition = new Vector3 (-1,0);
			pickaxe.transform.eulerAngles = new Vector3 (0, 0, -90);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * movementspeed * Time.deltaTime;
			pickaxe.transform.localPosition = new Vector3 (1,0);
			pickaxe.transform.eulerAngles = new Vector3 (0, 0, -90);
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.up * movementspeed * Time.deltaTime;
			pickaxe.transform.localPosition = new Vector3 (0,1);
			pickaxe.transform.eulerAngles = new Vector3 (0, 0, 0);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.down * movementspeed * Time.deltaTime;
			pickaxe.transform.localPosition = new Vector3 (0,-1);
			pickaxe.transform.eulerAngles = new Vector3 (0, 0, 0);
		}
		if (Input.GetKeyDown (KeyCode.Space)){
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown (KeyCode.F) ){
			StartCoroutine (attack ());
		}
	}

	IEnumerator attack(){
		pickaxe.SetActive (true);
		yield return new WaitForSeconds ((0.1f));
		pickaxe.SetActive (false);
	}


}
