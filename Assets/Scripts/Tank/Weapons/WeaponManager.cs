using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
    public GameObject Cannon;

    // Use this for initialization
    void Start ()
    {
        //GameObject cannon = (GameObject)Instantiate(UnityEngine.Resources.Load(DataManager.Instance.TankParams.DefaultCannon), transform.position, transform.rotation);
        GameObject cannon = GameObject.Instantiate(DataManager.Instance.TankParams.Cannon, transform.position, transform.rotation) as GameObject;
        cannon.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
