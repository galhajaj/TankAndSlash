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

    protected override void activate()
    {
        Tank.Instance.PutOnTurret("Cannon2");
        base.activate();
    }

    protected override void deactivate()
    {
        Tank.Instance.PutOffTurret();
        base.deactivate();
    }
}
