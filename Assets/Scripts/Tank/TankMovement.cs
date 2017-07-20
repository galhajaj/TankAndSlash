using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {

	private Rigidbody2D m_Rigidbody;              // Reference used to move the tank.

	public float speed = 3.0f;
	public float angularSpeed = 10.0f;
	private void Awake ()
	{
		m_Rigidbody = GetComponent<Rigidbody2D> ();
		m_Rigidbody.gravityScale = 0;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveForward ();
		Rotate ();
	}
	void Rotate()
	{
		float leftAngle = 0f;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			leftAngle = 100.0f;
		}
		else if (Input.GetKey (KeyCode.RightArrow)) {
			leftAngle = -100.0f;
		}
			
		float turn = angularSpeed * leftAngle * Time.deltaTime;
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation + turn);
	}
	void MoveForward()
	{
		Vector2 movement = new Vector2(0,0);
		if (Input.GetKey (KeyCode.UpArrow)) {
			movement = transform.up * speed * Time.deltaTime;
		}

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}
}