using System.Collections;
using System.Collections.Generic;

public class Chip_Const_Life : Chip
{
    protected override void install()
    {
        Tank.Instance.MaxLife += 1;
    }

    protected override void uninstall()
    {
        Tank.Instance.MaxLife -= 1;
    }
}
