using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorReinForce", menuName = "Items/ArmorReinForce", order = 4)]
public class ArmorReinForce : Items
{
    public override string GetDescription()
    {
        
        return base.GetDescription() + string.Format(
            "\n갑옷을 보강했습니다. 좀 더 많은 피해를 입어도 튼튼합니다. \n 최대체력 400 증가");

    }

    public override void theItemsEffect()
    {
        base.theItemsEffect();

        Player.initHealthPoint += 400;
    }



}
