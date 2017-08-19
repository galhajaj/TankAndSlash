using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip_Skill_Speed : Chip
{
    protected override void executeStart()
    {
        Tank.Instance.Speed *= 2;
    }

    protected override void executeEnd()
    {
        Tank.Instance.Speed /= 2;
    }
}
