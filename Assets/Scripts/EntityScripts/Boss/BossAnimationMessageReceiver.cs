using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationMessageReceiver : MonoBehaviour
{
    public void GetMessageEffect(int num)
    {
        transform.parent.GetComponent<EffectController>().GetMessageEffect(num);
    }

    public void AnimationMoveEnd()
    {
        transform.parent.GetComponent<BossPartentScripts>().AnimationMoveEnd();
    }
}
