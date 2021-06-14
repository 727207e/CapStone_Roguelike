using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavyParticle", menuName = "Items/HeavyParticle", order = 27)]
public class HeavyParticle : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 중입자를 사용하여 침투한 바이러스를 제거하고 회복합니다. \n 쿨타임 300초 \n 최대체력의 50% 회복");
    }
}
