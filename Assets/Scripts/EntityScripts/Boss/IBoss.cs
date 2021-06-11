using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoss
{
    void ShowTheBoss();

    void BossMove();

    void Attack();

    int BossHp { get; set; }
    List<GameObject> cameraMovingWalk_Camera { get; set; }

}
