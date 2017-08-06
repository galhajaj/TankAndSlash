using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooting : MonoBehaviour {

	public float ShootingForce = 50.0f;
	public GameObject Ammo;
	public static float directMaxDistance = 12.0f;	//meters

	void Start ()
    {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) { // left click
            GameObject shell = (GameObject)Instantiate (Ammo, transform.position, transform.rotation);
            Physics2D.IgnoreCollision (shell.GetComponent<Collider2D> (), this.GetComponentInParent<Collider2D> ());
            Physics2D.IgnoreCollision (shell.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());
			Vector2 force = shell.transform.up * ShootingForce;
			shell.GetComponent<Rigidbody2D> ().AddForce (force);
            Destroy (shell, 20);//find a good destruction formula
		}
	}
}
