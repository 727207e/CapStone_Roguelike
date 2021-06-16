using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dummy", menuName = "Items/Dummy", order = 9)]
public class Dummy : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n코인 더미를 발견했습니다. " +
            "\n 보유 재화가 500 늘어납니다.");


    }

    public override void theItemsEffect()
    {
        base.theItemsEffect();

        DataManager.Instance.data.Money += 500;

        DataManager.Instance.GameSave();
    }

}