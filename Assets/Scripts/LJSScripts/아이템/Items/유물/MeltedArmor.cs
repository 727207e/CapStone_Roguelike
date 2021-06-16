using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeltedArmor", menuName = "Items/MeltedArmor", order = 15)]
public class MeltedArmor : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n입고있는 방호복이 살짝 녹았습니다." +
            " \n 최대체력 200감소");
    }


    public override void theItemsEffect()
    {
        base.theItemsEffect();

        Player.initHealthPoint -= 200;
    }

}