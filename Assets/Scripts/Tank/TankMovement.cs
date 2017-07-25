using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {

	private Rigidbody2D m_Rigidbody;              // Reference used to move the tank.

//	public float speed = 3.0f;
//	public float angularSpeed = 10.0f;

	private float m_currentVelocity = 0f;
	private float m_currentRotateVec = 0f;
	private float MAX_VELOCITY = 10f;
	private float MAX_ANGULAR_VELOCITY = 1000f;

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
		if (Input.GetKey (KeyCode.A)) {
			m_currentRotateVec = Mathf.Min (m_currentRotateVec + 30f, MAX_ANGULAR_VELOCITY);
		} else if (Input.GetKey (KeyCode.D)) {
			m_currentRotateVec = Mathf.Max (m_currentRotateVec - 30f, -MAX_ANGULAR_VELOCITY);
		}
		else
		{
			m_currentRotateVec *= 0.9f;
			if (Mathf.Abs (m_currentRotateVec) < 0.1) {
				m_currentRotateVec = 0f;
			}
		}
			
		float turn =  m_currentRotateVec * Time.deltaTime;
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation + turn);
	}
	void MoveForward()
	{
		//W/S is read from config file?
		if (Input.GetKey (KeyCode.W)) {
			m_currentVelocity = Mathf.Min(m_currentVelocity + 10 * Time.deltaTime, MAX_VELOCITY);
		} else if (Input.GetKey (KeyCode.S)) {
			m_currentVelocity = Mathf.Max(m_currentVelocity - 10 * Time.deltaTime, -MAX_VELOCITY/2);
		} else {
			m_currentVelocity *= 0.9f;
			if (Mathf.Abs (m_currentVelocity) < 0.1) {
				m_currentVelocity = 0f;
			}
		}

		Vector2 movement = new Vector2(0,0);
		movement = transform.up * m_currentVelocity * Time.deltaTime;

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}
}