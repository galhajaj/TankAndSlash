using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneTankMovement : MonoBehaviour {

	public GameObject Ammo;

	private float ShootingForce = 4f;
	private float speed = 6f;

	private Rigidbody2D m_blueTank;
	private Rigidbody2D m_localRigidbody;
	private float lastShootingTime = 0f;
	// Use this for initialization
	void Start () {
		m_blueTank = GameObject.Find ("Tank").GetComponent<Rigidbody2D>();
		m_localRigidbody = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		lastShootingTime += Time.deltaTime;
//		if (Mathf.Sqrt (Mathf.Pow (m_blueTank.position.x - m_localRigidbody.position.x, 2) + Mathf.Pow (m_blueTank.position.y - m_localRigidbody.position.y, 2)) < 10*BarrelShooting.directMaxDistance) {
			if (lastShootingTime > 3) {
				Shoot ();
				lastShootingTime = 0f;
			}
//		}

		if (Mathf.Sqrt (Mathf.Pow (m_blueTank.position.x - m_localRigidbody.position.x, 2) + Mathf.Pow (m_blueTank.position.y - m_localRigidbody.position.y, 2)) > 2) {
			Move();
		}
				
	}

	void Shoot()
	{
		GameObject shell = (GameObject)Instantiate (Ammo, m_localRigidbody.position, m_localRigidbody.transform.rotation);
		shell.GetComponent<ShellParms> ().m_isHigh = false;
		Vector2 force = shell.transform.up * ShootingForce;
		shell.GetComponent<Rigidbody2D> ().AddForce (force);
		Destroy (shell, 100 / (force.x + force.y));
	}

	void Move()
	{
//		m_localRigidbody.velocity.Set (m_blueTank.position.x - m_localRigidbody.position.x, m_blueTank.position.y - m_localRigidbody.position.y);
//		m_localRigidbody.velocity.Set (30, 30);
//		m_localRigidbody.velocity.MovePosition(new Vector2(m_blueTank.position.x - m_localRigidbody.position.x, m_blueTank.position.y - m_localRigidbody.position.y));
		m_localRigidbody.MovePosition(Vector2.MoveTowards (m_localRigidbody.position, m_blueTank.position, speed * Time.deltaTime));
	}
}
