using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaghettiAlgorism", menuName = "Items/SpaghettiAlgorism", order = 21)]
public class SpaghettiAlgorism : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 대체 이런 유물은 어디에쓰죠? " +
            "\n 초당 얻는 에너지가 0.5만큼 감소합니다.");
    }

    public override void theItemsEffect()
    {
        base.theItemsEffect();
    }
}
