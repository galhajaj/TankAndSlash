using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting : Chip
{
    public override void Install()
    {
        Tank.Instance.InstallTurret("Cannon1");
        base.Install();
    }

    public override void Uninstall()
    {
        Tank.Instance.UninstallTurret();
        base.Uninstall();
    }
}
