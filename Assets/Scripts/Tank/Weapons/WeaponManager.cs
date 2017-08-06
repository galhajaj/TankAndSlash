using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject cannon = Instantiate(DataManager.Instance.TankParams.Cannon, transform.position, transform.rotation);
        cannon.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
