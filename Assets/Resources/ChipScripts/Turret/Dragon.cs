using System.Collections;
using System.Collections.Generic;

public class Dragon : Chip
{
    public override void Install()
    {
        Tank.Instance.InstallTurret("Cannon2");
        base.Install();
    }

    public override void Uninstall()
    {
        Tank.Instance.UninstallTurret();
        base.Uninstall();
    }
}
