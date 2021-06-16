using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialGloves", menuName = "Items/SpecialGloves", order = 22)]
public class SpecialGloves : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 연구진들이 바이러스에 최적화된 장갑을 " +
            "개발했습니다. \n 최대체력이 500 증가하고, 무적시간이 0.5초 증가합니다.");
    }
    public override void theItemsEffect()
    {
        base.theItemsEffect();

        Player.initHealthPoint += 500;

        Player.invincibleTime += 0.5f;
    }

}