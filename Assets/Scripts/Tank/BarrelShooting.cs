using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelShooting : MonoBehaviour {

	public float ShootingForce = 18.0f;
	public GameObject Ammo;
	public GameObject HighAmmo;
	public Transform Barrel;
	public static float directMaxDistance = 12.0f;	//meters

	void Start () {		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) { // left click
			//choosing the type of shooting
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = -(transform.position.x - Camera.main.transform.position.x);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
			float distance = Mathf.Sqrt (Mathf.Pow (worldPos.x - Barrel.position.x, 2) + Mathf.Pow (worldPos.y - Barrel.position.y, 2));

			if (distance < directMaxDistance) {
				//direct shooting
				GameObject shell2 = (GameObject)Instantiate (Ammo, Barrel.position, Barrel.rotation);
				Vector2 force = shell2.transform.up * ShootingForce;
				shell2.GetComponent<Rigidbody2D> ().AddForce (force);
				Destroy (shell2, 100 / (force.x + force.y));
			}
		}
	}
}
