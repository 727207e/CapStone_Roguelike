using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{

    //공격 범위 콜라이더
    Collider _coli;

    //공격 효과 List
    public List<ParticleSystem> EffectList;

    //공격 효과 등장 위치
    public List<GameObject> Effect_Pos;

    public void GetMessageEffect(int _effect_Num)
    {
        //4자리 숫자 중 

        // 천의 자리 -> 이펙트 리스트의 번째
        // 백의 자리 -> 이펙트 위치 리스트의 번째
        // 십의 자리 -> 이펙트가 오브젝트를 따라 다니는지 아닌지 판별하는 변수
        // 일의 자리 -> 만약 이펙트가 오브젝트를 따라 다닌다면 유지되는 시간
        
        int effect_Num = _effect_Num / 1000;

        int effect_Num_Pos = (_effect_Num % 1000) / 100;

        int effect_Num_RootPos = (_effect_Num % 100) / 10;

        int effect_Num_RootPos_time = _effect_Num % 10;


        //몸을 따라다니지 않는다
        if(effect_Num_RootPos == 0)
        {
            //지정된 파티클 생성(번째 파티클 지정, 지정된 위치에 생성)
            ParticleSystem theEffect = Instantiate(EffectList[effect_Num - 1],
                Effect_Pos[effect_Num_Pos - 1].transform.position, Quaternion.identity).GetComponent<ParticleSystem>();

        }

        //몸을 따라간다
        if(effect_Num_RootPos == 1)
        {
            ParticleSystem theEffect = Instantiate(EffectList[effect_Num - 1],
                Effect_Pos[effect_Num_Pos - 1].transform.position, Quaternion.identity).GetComponent<ParticleSystem>();

            theEffect.transform.parent = Effect_Pos[effect_Num_Pos - 1].transform;

            //시간이 되면 종료
            Destroy(theEffect.gameObject, effect_Num_RootPos_time);
        }

        //공격 콜라이더 호출
        Hitfunc(effect_Num_Pos);

    }

    void Hitfunc(int effect_Num_Pos)
    {
        _coli = Effect_Pos[effect_Num_Pos - 1].GetComponent<Collider>();

        //콜라이더 활성화
        _coli.enabled = true;

        Invoke("HitfuncOff", 0.5f);
    }

    void HitfuncOff()
    {
        //비활성화
        _coli.enabled = false;
    }

}
