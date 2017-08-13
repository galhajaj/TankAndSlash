using System.Collections;
using System.Collections.Generic;

public class Dragon : Chip
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
        Tank.Instance.PutOnTurret("Cannon2");
        base.Activate();
    }

    public override void Deactivate()
    {
        Tank.Instance.PutOffTurret();
        base.Deactivate();
    }
}
