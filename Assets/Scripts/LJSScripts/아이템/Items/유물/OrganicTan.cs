using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrganicTan", menuName = "Items/OrganicTan", order = 14)]
public class OrganicTan : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n머신건의 탄환에 유기물 분해능력을 " +
            "장착하여 \n 머신건의 공격력이 2 상승합니다.");
    }


    public override void theItemsEffect()
    {
        base.theItemsEffect();

        MachinGun.rifleDamage += 2;
    }

}
