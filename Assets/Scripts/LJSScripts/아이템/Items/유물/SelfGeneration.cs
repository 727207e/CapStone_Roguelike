using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfGeneration", menuName = "Items/Generation", order = 2)]
public class SelfGeneration : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 자가 발전이 가능해졌습니다. " +
            "\n 에너지가 초당 1만큼 추가로 회복됩니다.");
    }

    public override void theItemsEffect()
    {
        base.theItemsEffect();
    }
}