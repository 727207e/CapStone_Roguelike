using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TheifsGloves", menuName = "Items/TheifsGloves", order = 23)]
public class TheifsGloves : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n A급 이하 랜덤한 유물을 2개 가져옵니다." +
            " \n 저주받은 유물도 포함됩니다.");
    }
}
