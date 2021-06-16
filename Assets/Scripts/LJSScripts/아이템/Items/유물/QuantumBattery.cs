using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuantumBattery", menuName = "Items/QuantumBattery", order = 18)]
public class QuantumBattery : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n양자 배터리를 장착하여" +
            " \n 에너지가 초당 0.5만큼 추가로 회복됩니다.");
    }


}