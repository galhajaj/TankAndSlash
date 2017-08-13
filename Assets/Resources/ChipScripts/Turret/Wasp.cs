using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : Chip
{
    public override void Install()
    {
        Tank.Instance.InstallTurret("Cannon3");
        base.Install();
    }

    public override void Uninstall()
    {
        Tank.Instance.UninstallTurret();
        base.Uninstall();
    }
}
