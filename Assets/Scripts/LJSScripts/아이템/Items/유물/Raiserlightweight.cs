using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Raiserlightweight", menuName = "Items/Raiserlightweight", order = 20)]
public class Raiserlightweight : Items
{
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n 레이저의 능력이 환경에 적합하게 변화합니다. " +
            "\n 레이저 쿨타임 2초 감소, 에너지 소모량 20 감소\n 데미지 30 감소");
    }


    public override void theItemsEffect()
    {
        base.theItemsEffect();

        Player.sklUI.cooldown2 -= 2;

        //에너지 소모량

        Player.skill_2_Damage -= 30;
    }

}
