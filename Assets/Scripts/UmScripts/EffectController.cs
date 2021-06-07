using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{

    //공격 범위 콜라이더
    Collider _coli;

    //공격 효과 List
    public List<GameObject> EffectList;

    //공격 효과 등장 위치
    public List<GameObject> Effect_Pos;

    GameObject theEffect;
    public void GetMessageEffect(int _effect_Num)
    {
        //4자리 숫자 중 

        // effect_Num -> 이펙트 리스트의 번째
        // effect_Num_Pos -> 이펙트 위치 리스트의 번째
        // effect_Num_RootPos -> 이펙트가 오브젝트를 따라 다니는지 아닌지 판별하는 변수(0은 안 따라감)
        // effect_Num_Attack_Special -> 공격 함수 특수 번째

        int effect_Num = _effect_Num / 1000;

        int effect_Num_Pos = (_effect_Num % 1000) / 100;

        int effect_Num_RootPos = (_effect_Num % 100) / 10;

        int effect_Num_Attack_Special = _effect_Num % 10;

        //몸을 따라다니지 않는다
        if (effect_Num_RootPos == 0)
        {
            InstantiateParticle(effect_Num, effect_Num_Pos,false);
        }


        //몸을 따라간다
        if (effect_Num_RootPos == 1)
        {
            InstantiateParticle(effect_Num, effect_Num_Pos,true);

            theEffect.transform.parent = Effect_Pos[effect_Num_Pos - 1].transform;

        }

        //공격의 특수효과 발동
        GetComponent<BossPartentScripts>().AttackSpecial(effect_Num_Attack_Special);

        //공격 콜라이더 호출
        Hitfunc(effect_Num_Pos);

    }

    public void InstantiateParticle(int _effect_Num, int _effect_Num_Pos , bool Root)
    {
        //지정된 파티클 생성(번째 파티클 지정, 지정된 위치에 생성)
        theEffect = Instantiate(EffectList[_effect_Num - 1],
            Effect_Pos[_effect_Num_Pos - 1].transform.position, 
            Effect_Pos[_effect_Num_Pos - 1].transform.rotation) as GameObject;

        //생성위치를 따라다닌다.
        if (Root)
        {
            theEffect.transform.parent = Effect_Pos[_effect_Num_Pos - 1].transform;
        }

    }

    public void Hitfunc(int effect_Num_Pos)
    {
        _coli = Effect_Pos[effect_Num_Pos - 1].GetComponent<Collider>();

        //콜라이더가 없다면
        if(_coli == null)
        {
            //콜라이더가 없다면 자식이 콜라이더를 가지고 있는 형식. 그 자식콜라이더 키기.
            _coli = Effect_Pos[effect_Num_Pos - 1].transform.Find("AttackCollider")
                .GetComponent<Collider>();

        }

        //콜라이더 활성화
        _coli.enabled = true;

        Invoke("HitfuncOff", 0.5f);
    }

    public void HitfuncOff()
    {
        //비활성화
        _coli.enabled = false;
    }

}
