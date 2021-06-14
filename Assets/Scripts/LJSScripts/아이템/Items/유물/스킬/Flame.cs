using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flame", menuName = "Items/Flame", order = 25)]
public class Flame : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 초고온의 불꽃으로 바이러스를 박멸합니다. \n 쿨타임 30초, 데미지 100 \n 에너지소모량 50");
    }
}