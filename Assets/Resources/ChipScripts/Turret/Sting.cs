using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting : Chip
{
    public override void Install()
    {
        base.Install();
    }

    public override void Uninstall()
    {
        base.Uninstall();
    }

    public override void Activate()
    {
        Tank.Instance.PutOnTurret("Cannon1");
        base.Activate();
    }

    public override void Deactivate()
    {
        Tank.Instance.PutOffTurret();
        base.Deactivate();
    }
}
