using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FastTan", menuName = "Items/FastTan", order = 11)]
public class FastTan : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n총알이 더욱 빨라집니다. \n 탄환 속도 20% 증가");
    }
}