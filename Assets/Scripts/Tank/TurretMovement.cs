using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour {

	public float AngularVel = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = -(transform.position.x - Camera.main.transform.position.x);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

		float speed = -AngularVel;
		float step = speed * Time.deltaTime;

		float angle = Mathf.Atan2 (transform.position.x - worldPos.x, transform.position.y - worldPos.y) * Mathf.Rad2Deg;

		Quaternion rotateTo = Quaternion.AngleAxis(-angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, step);

	}
}


