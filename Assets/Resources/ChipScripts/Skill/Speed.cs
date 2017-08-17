using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Chip
{
    new public float CostPerSecond = 0.5F;

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
        Tank.Instance.Speed *= 2;
        base.ExecuteStart();
    }

    public override void ExecuteContinues()
    {
        Tank.Instance.Power -= Time.deltaTime * CostPerSecond;
        base.ExecuteContinues();
    }

    public override void ExecuteEnd()
    {
        Tank.Instance.Speed /= 2;
        base.ExecuteEnd();
    }
}
