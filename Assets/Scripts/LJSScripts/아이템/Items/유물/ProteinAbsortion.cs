using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProteinAbsortion", menuName = "Items/ProteinAbsortion", order = 17)]
public class ProteinAbsortion : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n공격을 할 때마다 데미지의 " +
            "70%만큼 체력을 회복합니다. \n 단, 최대체력이 700 감소합니다.");
    }
}
