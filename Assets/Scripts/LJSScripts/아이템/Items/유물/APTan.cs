using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "APTan", menuName = "Items/APTan", order = 3)]
public class APTan : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n이전보다 더욱 좋은 성능의 철갑탄을 사용합니다." +
            " \n 권총 공격력 5 증가" );
    }

    public override void theItemsEffect()
    {
        base.theItemsEffect();

        HandGun.pistolDamage += 5;
    }

}
