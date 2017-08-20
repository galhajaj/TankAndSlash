using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public bool IsAllowedToMove = true;
    private Rigidbody2D _rigidBody;

//    public float Speed = 30f;
//    public float AngularVelocity = 3f;
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
    }
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (!IsAllowedToMove)
            return;

        MoveForward();
        MoveBackward();
        MoveRight();
        MoveLeft();
        RotateRight();
        RotateLeft();
	}

    void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidBody.AddForce(_rigidBody.transform.up * Tank.Instance.Speed);
        }
    }
        
    void MoveBackward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _rigidBody.AddForce(_rigidBody.transform.up * (-1) * Tank.Instance.Speed / 2);
        }
    }

    void MoveRight()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _rigidBody.AddForce(_rigidBody.transform.right * Tank.Instance.Speed);
        }
    }

    void MoveLeft()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _rigidBody.AddForce(_rigidBody.transform.right * (-1) * Tank.Instance.Speed);
        }
    }

    void RotateLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rigidBody.AddTorque(Tank.Instance.AngularVelocity);
        }
    }

    void RotateRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //_rigidBody.transform.Rotate((-1) * Vector3.forward * Tank.Instance.TankParams.AngularVelocity);
            _rigidBody.AddTorque(-Tank.Instance.AngularVelocity);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //_rigidBody.freezeRotation = true;
    }

}
