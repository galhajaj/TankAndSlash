using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBehavior : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
//		Rigidbody2D selfRigidBody =  this.gameObject.GetComponent<Rigidbody2D> ();
//		Rigidbody2D collidedRigidBody =  other.GetComponent<Collider2D>().gameObject.GetComponent<Rigidbody2D> ();
		//Event: explosion
		Destroy(this.gameObject);

	}
}
