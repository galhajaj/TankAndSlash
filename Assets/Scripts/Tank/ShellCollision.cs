using UnityEngine;
using System.Collections;

public class ShellCollision : MonoBehaviour 
{
	void Start () 
	{
	}
	// ======================================================================================================================================== //
	void Update () 
	{
	}
	// ======================================================================================================================================== //
/*	void OnCollisionEnter2D(Collision2D collision)
	{
		Rigidbody2D selfRigidBody =  this.gameObject.GetComponent<Rigidbody2D> ();
		Rigidbody2D collidedRigidBody =  collision.collider.gameObject.GetComponent<Rigidbody2D> ();
		if (collidedRigidBody != null && selfRigidBody != null) 
		{
			selfRigidBody.MovePosition (new Vector2 (0, 0));
		}
		//Instantiate(ExplosionObj, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
*/
}
