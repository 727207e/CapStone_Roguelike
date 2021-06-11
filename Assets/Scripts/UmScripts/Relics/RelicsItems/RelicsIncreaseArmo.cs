using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicsIncreaseArmo : RelicsParentSc
{
    public int Value;



    public override void theRelicsEffect()
    {
        base.theRelicsEffect();

        PlayerControllHp(Value);
    }
}
