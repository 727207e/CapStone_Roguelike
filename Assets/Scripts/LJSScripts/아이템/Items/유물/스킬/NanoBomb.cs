using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NanoBomb", menuName = "Items/NanoBomb", order = 24)]
public class NanoBomb : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 나노폭탄으로 바이러스를 박멸합니다. \n 쿨타임 8초, 데미지 40 \n 에너지소모량 30");
    }
}
