using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Precise", menuName = "Items/Precise", order = 16)]
public class Precise : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n한발 한발을 더욱 정밀하게 사격합니다." +
            "\n 그만큼 발사속도는 감소합니다. \n 공격력 20%증가, 공격속도 10% 감소");
    }

    public override void theItemsEffect()
    {
        base.theItemsEffect();

        HandGun.pistolDamage += 5;
    }

}
