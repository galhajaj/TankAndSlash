using System.Collections;
using System.Collections.Generic;

public class Life : Chip
{
    public override void Install()
    {
        DataManager.Instance.MaxLife += 1;
        base.Install();
    }

    public override void Uninstall()
    {
        DataManager.Instance.MaxLife -= 1;
        base.Uninstall();
    }
}
