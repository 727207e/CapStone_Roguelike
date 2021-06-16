using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AuxiliaryCannon", menuName = "Items/AuxiliaryCannon", order = 6)]
public class AuxiliaryCannon : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n캐논볼을 발사할 때 탄알이 2개가 됩니다. " +
            "\n 단, 데미지가 15 감소합니다.");
    }

    public override void theItemsEffect()
    {
        base.theItemsEffect();

        HandGun.pistolDamage += 5;
    }

}