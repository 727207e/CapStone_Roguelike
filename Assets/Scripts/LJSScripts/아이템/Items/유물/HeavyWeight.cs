using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavyWeight", menuName = "Items/HeavyWeight", order = 12)]
public class HeavyWeight : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n.평행무게 유지장치에 문제가 생겼습니다. " +
            "\n 이동속도 1 감소");
    }
    public override void theItemsEffect()
    {
        base.theItemsEffect();

        Player.walkSpeed -= 1;
    }

}