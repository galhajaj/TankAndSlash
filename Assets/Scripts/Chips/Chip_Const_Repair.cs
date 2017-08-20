﻿using System.Collections;
using System.Collections.Generic;

public class Chip_Const_Repair : Chip
{
    protected override void install()
    {
        Tank.Instance.LifeRegenerationRate += 0.25F;
    }

    protected override void uninstall()
    {
        Tank.Instance.LifeRegenerationRate -= 0.25F;
    }
}