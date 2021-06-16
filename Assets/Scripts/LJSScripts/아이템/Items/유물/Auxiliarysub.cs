using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Auxiliarysub", menuName = "Items/Auxiliarysub", order = 7)]
public class Auxiliarysub : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n기본총을 발사할 때 탄알이 2개가 됩니다. " +
            "\n 단, 데미지가 1 감소합니다.");
    }


    public override void theItemsEffect()
    {
        base.theItemsEffect();

        HandGun.pistolDamage += 5;
    }

}