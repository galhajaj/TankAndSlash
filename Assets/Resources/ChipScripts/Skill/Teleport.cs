using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Chip
{
    new public int Cost = 2;

    public override void Install()
    {
        base.Install();
    }

    public override void Uninstall()
    {
        base.Uninstall();
    }

    public override void ExecuteStart()
    {
        GameObject tankObj = Tank.Instance.gameObject;
        tankObj.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
            tankObj.transform.position.z);
        Tank.Instance.Power -= Cost;
        base.ExecuteStart();
    }
}
