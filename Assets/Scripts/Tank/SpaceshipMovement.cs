using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
    }

	void Start ()
    {
		
	}
	
    void FixedUpdate()
    {
        if (!Tank.Instance.IsActive)
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
            _rigidBody.AddForce(_rigidBody.transform.up * Tank.Instance.Thrust);
        }
    }
        
    void MoveBackward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _rigidBody.AddForce(_rigidBody.transform.up * (-1) * Tank.Instance.ReverseThrust);
        }
    }

    void MoveRight()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _rigidBody.AddForce(_rigidBody.transform.right * Tank.Instance.SidewaysThrust);
        }
    }

    void MoveLeft()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _rigidBody.AddForce(_rigidBody.transform.right * (-1) * Tank.Instance.SidewaysThrust);
        }
    }

    void RotateLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rigidBody.AddTorque(Tank.Instance.AngularThrust);
        }
    }

    void RotateRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _rigidBody.AddTorque(-Tank.Instance.AngularThrust);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //_rigidBody.freezeRotation = true;
    }

}
