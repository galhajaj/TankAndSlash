using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Transform _blueTank;
    private float _angularVel;
    private float _shootingInterval;    //seconds
    private float _timeToNextShot;

    public float ShootingForce = 50.0f;
    public GameObject Ammo;

    // Use this for initialization
    void Start ()
    {
        _blueTank = GameObject.Find("Spaceship").GetComponent<Rigidbody2D>().transform;
        //Do we want enemy parms in the DataManager as well?   _angularVel = DataManager.Instance.
        _angularVel = 30;
        _shootingInterval = 2;
        _timeToNextShot = _shootingInterval;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rotating();
        Shooting();
	}

    void Shooting()
    {
        _timeToNextShot -= Time.deltaTime;
        if (_timeToNextShot < 0)
        {
            _timeToNextShot = _shootingInterval;
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject shell = (GameObject)Instantiate (Ammo, transform.position, transform.rotation);
        Physics2D.IgnoreCollision (shell.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());
        Vector2 force = shell.transform.up * ShootingForce;
        shell.GetComponent<Rigidbody2D> ().AddForce (force);
        Destroy (shell, 20);//find a good destruction formula
    }

    void Rotating()
    {
        float speed = -_angularVel;
        float step = speed * Time.deltaTime;

        float angle = Mathf.Atan2 (transform.position.x - _blueTank.position.x, transform.position.y - _blueTank.position.y) * Mathf.Rad2Deg;

        Quaternion rotateTo = Quaternion.AngleAxis(-angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, step);
    }
}
