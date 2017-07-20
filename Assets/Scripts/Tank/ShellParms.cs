using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellParms : MonoBehaviour {

	public bool m_isHigh = false;
	public float explosionDistance;
	public Vector2 startingPosition;

	private Rigidbody2D m_rigidBody;

	// Use this for initialization
	void Start () {
		m_rigidBody = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isHigh)
		{
			if (Mathf.Sqrt(Mathf.Pow(startingPosition.x - m_rigidBody.position.x,2) + Mathf.Pow(startingPosition.y - m_rigidBody.position.y,2)) > explosionDistance) {
				m_rigidBody.MovePosition (new Vector2 (0, 0));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

				if (m_isHigh)
				{
					if (Mathf.Sqrt(Mathf.Pow(startingPosition.x - m_rigidBody.position.x,2) + Mathf.Pow(startingPosition.y - m_rigidBody.position.y,2)) < explosionDistance) {

						return;
		//m_rigidBody.MovePosition (new Vector2 (0, 0));
					}
				}

		Rigidbody2D selfRigidBody =  this.gameObject.GetComponent<Rigidbody2D> ();
		Rigidbody2D collidedRigidBody =  other.GetComponent<Collider2D>().gameObject.GetComponent<Rigidbody2D> ();
		selfRigidBody.MovePosition (new Vector2 (0, 0));

		selfRigidBody.position.Set (0, 0);

		Destroy(this.gameObject);

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
//		if (m_isHigh)
//		{
//			if (Mathf.Sqrt(Mathf.Pow(startingPosition.x - m_rigidBody.position.x,2) + Mathf.Pow(startingPosition.y - m_rigidBody.position.y,2)) < explosionDistance) {

//				return;
				//m_rigidBody.MovePosition (new Vector2 (0, 0));
//			}
//		}

		Rigidbody2D selfRigidBody =  this.gameObject.GetComponent<Rigidbody2D> ();
		Rigidbody2D collidedRigidBody =  collision.collider.gameObject.GetComponent<Rigidbody2D> ();
		selfRigidBody.MovePosition (new Vector2 (0, 0));

		Destroy(this);
	}

}
