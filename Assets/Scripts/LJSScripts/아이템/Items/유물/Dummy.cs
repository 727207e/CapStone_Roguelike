using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dummy", menuName = "Items/Dummy", order = 9)]
public class Dummy : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n코인 더미를 발견했습니다. \n 보유 재화가 500 늘어납니다.");
    }
}