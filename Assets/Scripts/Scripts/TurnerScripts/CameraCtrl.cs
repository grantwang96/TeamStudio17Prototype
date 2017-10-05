using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

	public GameObject p1;
	public GameObject p2;

	public GameObject main_camera;

	private Transform p1transform;
	private Transform p2transform;

	public GameObject p1cam;
	public GameObject p2cam;

	public float distanceCheck;

	void Start () {
		p1transform = p1.transform;
		p2transform = p2.transform;

		main_camera.SetActive (false);
		p1cam.SetActive (true);
		p2cam.SetActive (true);
			
	}

	// Update is called once per frame
	void Update () {
		
		float distance = Vector3.Distance (p1transform.position, p2transform.position);
		float cam_x = p1transform.position.x + (p2transform.position.x - p1transform.position.x) / 2;
		float cam_y = p1transform.position.y + (p2transform.position.y - p1transform.position.y) / 2;
		Vector3 midpoint = new Vector3 (cam_x, cam_y, main_camera.transform.position.z);
		
		if (distance < distanceCheck){
			Debug.Log ("DistanceIF: " + distance);
			main_camera.SetActive (true);
			p1cam.SetActive (false);
			p2cam.SetActive (false);
			main_camera.transform.position = midpoint;

		}
		else{
			Debug.Log ("DistanceELSE: " + distance);
			main_camera.SetActive (false);
			p1cam.SetActive (true);
			p2cam.SetActive (true);
		}
		
	}
}
