using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuickTan", menuName = "Items/QuickTan", order = 19)]
public class QuickTan : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 재장전에 유리한 탄창을 사용합니다. " +
            "\n 재장전 속도가 10% 빨라집니다.");
    }
}
