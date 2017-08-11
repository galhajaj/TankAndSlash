using System.Collections;
using System.Collections.Generic;

public class Life : Chip
{
    public override void Install()
    {
        DataManager.Instance.TankParams.MaxLife += 1;
        base.Install();
    }

    public override void Uninstall()
    {
        DataManager.Instance.TankParams.MaxLife -= 1;
        base.Uninstall();
    }
}
