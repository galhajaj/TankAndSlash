using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnTriggerEnter2D(Collider2D other)
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.name == "Spaceship(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
