using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicsParentSc : MonoBehaviour
{
    public Sprite theRelicsImage_Icon;
    public Sprite theRelicsImage_Card;

    public GameObject Prefabs_Card_Obj;
    public GameObject Canvas_Card;

    protected msOneHandGun HandGun;
    protected msMachineGun MachinGun;
    protected msCannon Cannon;
    protected msPlayerControllerNew Player;

    private int HandGun_Ammo;
    private float HandGun_ReloadTime;

    private int MachinGun_Ammo;
    private float MachinGun_ReloadTime;

    private int Cannon_Ammo;
    private float Cannon_ReloadTime;

    GameObject theCardObj;


    // Start is called before the first frame update
    void Start()
    {
        HandGun = GameObject.Find("MainCharacterSys").transform.Find("MainPlayerCharacter").
            transform.Find("WeaponHolder").transform.Find("WeaponPivot").transform.Find("Pistol").
            GetComponent<msOneHandGun>();

        MachinGun = GameObject.Find("MainCharacterSys").transform.Find("MainPlayerCharacter").
             transform.Find("WeaponHolder").transform.Find("WeaponPivot").transform.Find("Rifle").
             GetComponent<msMachineGun>();

        Cannon = GameObject.Find("MainCharacterSys").transform.Find("MainPlayerCharacter").
            transform.Find("WeaponHolder").transform.Find("WeaponPivot").transform.Find("Cannon").
            GetComponent<msCannon>();

        Player = GameObject.Find("MainCharacterSys").transform.Find("MainPlayerCharacter").
            GetComponent<msPlayerControllerNew>();

        Canvas_Card = GameObject.Find("CanvasCard").gameObject;


        HandGun_Ammo = HandGun.fullAmmo;
        HandGun_ReloadTime = HandGun.reloadTime;


        MachinGun_Ammo = MachinGun.fullAmmo;
        MachinGun_ReloadTime = MachinGun.reloadTime;


        Cannon_Ammo = Cannon.fullAmmo;
        Cannon_ReloadTime = Cannon.reloadTime;
    }

    public void InstantiateCard()
    {
        //생성이후 바로 함수 실행시 생길 버그 방지
        Start();

        //카드 생성
        theCardObj = Instantiate(Prefabs_Card_Obj) as GameObject;

        //카드 이미지 대입
        theCardObj.GetComponent<Image>().sprite = theRelicsImage_Card;

        //캔버스에 포함
        theCardObj.transform.SetParent(Canvas_Card.transform);

        //버튼 기능 추가
        theCardObj.GetComponent<Button>().onClick.AddListener(BtnSelectMe);
    }

    public void BtnSelectMe()
    {
        //홀더를 가져온다.
        RelicsHolderSc holder = GameObject.Find("RelicsHolderObj").GetComponent<RelicsHolderSc>();

        //홀더를 부모로 만들어서 오브젝트 파괴 방지 
        transform.parent = holder.transform;

        //자신을 홀더에 포함(먹은 오브젝트 모아두기)
        holder.relicsList.Add(this);

        //효과 발동
        theRelicsEffect();

        //카드 파괴
        if (theCardObj != null)
            Destroy(theCardObj);
    }

    /// 
    /// ///////////////////////////////////////////////////////////////////기능
    /// 
    protected void HandControllAmmo(int num)
    {
        HandGun_Ammo += num;
    }

    protected void HandControllreloadTime(float num)
    {
        HandGun_ReloadTime += num;
    }

    protected void PlayerControllHp(int num)
    {
        Player.initHealthPoint += num;
    }

    public virtual void theRelicsEffect()
    {

    }
}
