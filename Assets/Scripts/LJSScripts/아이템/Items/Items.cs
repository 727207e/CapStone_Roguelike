using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality { C, B, A, S ,Curse}

public abstract class Items : ScriptableObject, IDescribable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;
    private SlotScript slot;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    [SerializeField]
    public Sprite cardImage;

    [SerializeField]
    public int weight;

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
    // Start is called before the first frame update
    public void Start()
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


        HandGun_Ammo = HandGun.fullAmmo;
        HandGun_ReloadTime = HandGun.reloadTime;


        MachinGun_Ammo = MachinGun.fullAmmo;
        MachinGun_ReloadTime = MachinGun.reloadTime;


        Cannon_Ammo = Cannon.fullAmmo;
        Cannon_ReloadTime = Cannon.reloadTime;

        if(HandGun == null)
        {
            Debug.Log("the target is null");
        }
    }


    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    // 아이템이 중첩될 수 있는 개수
    // 예) 소모성 물약의 경우 한개의 Slot에 여러개가
    //     중첩되어서 보관될 수 있음.
    public int StackSize
    {
        get
        {
            return stackSize;
        }
    }

    protected SlotScript Slot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public virtual string GetDescription()
    {
        string color = string.Empty;

        switch (quality)
        {
            case Quality.C:
                color = "#d6d6d6";
                break;
            case Quality.B:
                color = "#00ff00ff";
                break;
            case Quality.A:
                color = "#0000ffff";
                break;
            case Quality.S:
                color = "#800080ff";
                break;
            case Quality.Curse:
                color = "#FF0000";
                break;
        }

        return string.Format("<color={0}>{1}</color>", color, title);
    }

    public virtual void theItemsEffect()
    {

    }

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



}