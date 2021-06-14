using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GravityField", menuName = "Items/GravityField", order = 26)]
public class GravityField : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 중력장으로 주변의 물질들을 튕겨냅니다. \n 쿨타임 12초, 데미지 20 \n 에너지소모량 20");
    }
}