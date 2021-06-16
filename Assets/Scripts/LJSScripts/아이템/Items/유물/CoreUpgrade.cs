using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoreUpgrade", menuName = "Items/CoreUpgrade", order = 8)]
public class CoreUpgrade : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n코어 에너지 발생량을 " +
            "비약적으로 증가시킵니다.. \n 최대 에너지가 50 증가합니다.");
    }


    public override void theItemsEffect()
    {
        base.theItemsEffect();

        Player.initAbilityPoint += 50f;
    }

}
