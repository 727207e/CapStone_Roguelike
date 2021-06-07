using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClick : MonoBehaviour
{
    public string type;
    public float per;
    public float duration;
    public Sprite icon;

    public void Click()
    {
        BuffMgr.instance.CreateBuff(type, per, duration, icon);
    }
}
