using System.Collections;
using System.Collections.Generic;

public class Life : Chip
{
    public override void Install()
    {
        Tank.Instance.MaxLife += 1;
        base.Install();
    }

    public override void Uninstall()
    {
        Tank.Instance.MaxLife -= 1;
        base.Uninstall();
    }
}
