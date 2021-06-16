using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyLimit", menuName = "Items/EnergyLimit", order = 10)]
public class EnergyLimit : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n캐논의 에너지 응집률을 높히는 대신 반동이" +
            " 커졌습니다. \n 캐논 공격력이 40 늘어납니다.\n 탄창이 3발 줄어듭니다.");
    }
    public override void theItemsEffect()
    {
        base.theItemsEffect();

        Cannon.cannonDamage += 40;

        Cannon.fullAmmo -= 3;
    }

}