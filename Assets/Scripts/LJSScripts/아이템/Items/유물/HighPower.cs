using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighPower", menuName = "Items/HighPower", order = 13)]
public class HighPower : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n피스톨의 에너지 압축률을 늘리며 " +
            "\n 다른 부분에 영향을 끼치지 않는 방법을 알아냈습니다. \n 피스톨 공격력 10증가");
    }


    public override void theItemsEffect()
    {
        base.theItemsEffect();

        HandGun.pistolDamage += 10;
    }

}
