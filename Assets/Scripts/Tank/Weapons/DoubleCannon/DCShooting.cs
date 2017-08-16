using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCShooting : MonoBehaviour {

    public float ShootingForce = 50.0f;
    public GameObject Ammo;
    public static float directMaxDistance = 12.0f;  //meters

    void Start ()
    {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown (0)) { // left click
            GameObject shell1 = (GameObject)Instantiate (Ammo, transform.position, transform.rotation);
            shell1.transform.Translate(Vector3.left/10);
            Physics2D.IgnoreCollision (shell1.GetComponent<Collider2D> (), this.GetComponentInParent<Collider2D> ());
            Physics2D.IgnoreCollision (shell1.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());
            Vector2 force1 = shell1.transform.up * ShootingForce;
            shell1.GetComponent<Rigidbody2D> ().AddForce (force1);
            Destroy (shell1, 20);//find a good destruction formula

            GameObject shell2 = (GameObject)Instantiate (Ammo, transform.position, transform.rotation);
            shell2.transform.Translate(Vector3.right/10);
            Physics2D.IgnoreCollision (shell2.GetComponent<Collider2D> (), this.GetComponentInParent<Collider2D> ());
            Physics2D.IgnoreCollision (shell2.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());
            Physics2D.IgnoreCollision (shell2.GetComponent<Collider2D> (), shell1.GetComponent<Collider2D> ());
            Vector2 force2 = shell2.transform.up * ShootingForce;
            shell2.GetComponent<Rigidbody2D> ().AddForce (force2);
            Destroy (shell2, 20);//find a good destruction formula
        }
    }
}
