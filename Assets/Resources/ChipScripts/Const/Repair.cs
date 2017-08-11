using System.Collections;
using System.Collections.Generic;

public class Repair : Chip
{
    public override void Install()
    {
        DataManager.Instance.LifeRegenerationRate += 0.25F;
        base.Install();
    }

    public override void Uninstall()
    {
        DataManager.Instance.LifeRegenerationRate -= 0.25F;
        base.Uninstall();
    }
}