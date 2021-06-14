using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour
{

    /// <summary>
    /// ///////////////////////////////////////////// <<<<<<<<<<<<<<<<<<중요>>>>>>>>>>>>>>>
    /// </summary>
    //씬이동해도 가방에 있는 아이템들의 정보가 사라지는지 확인해볼것

    //////////////////////////////////////////////////////

    private static InventoryScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //씬 전환이 되어도 파괴되지 않는다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //씬 전환이후 그 씬에도 gamemanager가 있을수 있음.
            //따라서 새로운 씬의 gamemanager 파괴
            Destroy(this.gameObject);

        }
    }

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();


            }
            return instance;
        }

        set
        {

            instance = value;
        }
    }


    private List<Bag> bags = new List<Bag>();
    public bool CanAddBag
    {
        get { return bags.Count < 1; }
    }


    [SerializeField]
    private BagButton[] bagButtons;




    // 테스트를 위한 용도
    [SerializeField]
    public List<Items> item;


    private void Update()
    {

        // I 키를 누르면 가방이 BagButton에 추가됨.
        if (Input.GetKeyDown(KeyCode.I))
        {
            Bag bag = (Bag)Instantiate(item[0]);
            bag.Initalize(20);
            bag.Use();
        }

        /*
        // K 키를 누르면 가방에 가방아이템을 넣는다. 
       // if (Input.GetKeyDown(KeyCode.K))
        //{
       //     Bag bag = (Bag)Instantiate(items[0]);
        //    bag.Initalize(20);
       //     AddItem(bag);
       // }

        
        // L키를 누르면 가방에 자가재생장치 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.J))
        {

           SelfGeneration generation = (SelfGeneration)Instantiate(items[1]);

            AddItem(generation);
        }

        // L키를 누르면 가방에 철갑탄 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.L))
        {

            APTan tan = (APTan)Instantiate(items[2]);

            AddItem(tan);
        }

        // L키를 누르면 가방에 갑옷보강 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.Z))
        {

            ArmorReinForce reinForce = (ArmorReinForce)Instantiate(items[3]);

            AddItem(reinForce);
        }

        // L키를 누르면 가방에 보조기본탄환 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.X))
        {

           AuxiliaryBasic basic = (AuxiliaryBasic)Instantiate(items[4]);

            AddItem(basic);
        }

        // L키를 누르면 가방에 보조캐논탄환 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.C))
        {

           AuxiliaryCannon Cannon = (AuxiliaryCannon)Instantiate(items[5]);

            AddItem(Cannon);
        }

        // L키를 누르면 가방에 보조머신건탄환 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.V))
        {

            Auxiliarysub sub = (Auxiliarysub)Instantiate(items[6]);

            AddItem(sub);
        }

        // L키를 누르면 가방에 코어 업그레이드 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.B))
        {

            CoreUpgrade Upgrade = (CoreUpgrade)Instantiate(items[7]);

            AddItem(Upgrade);
        }

        // L키를 누르면 가방에 코인더미 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.N))
        {

            Dummy dummy = (Dummy)Instantiate(items[8]);

            AddItem(dummy);
        }

        // L키를 누르면 가방에 에너지한계돌파 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.M))
        {

            EnergyLimit Limit = (EnergyLimit)Instantiate(items[9]);

            AddItem(Limit);
        }

        // L키를 누르면 가방에 탄환가속 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.Y))
        {

            FastTan tan = (FastTan)Instantiate(items[10]);

            AddItem(tan);
        }

        // L키를 누르면 가방에 과다체중 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.U))
        {

            HeavyWeight Weight = (HeavyWeight)Instantiate(items[11]);

            AddItem(Weight);
        }

        // L키를 누르면 가방에 고출력파워 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.O))
        {

            HighPower Power = (HighPower)Instantiate(items[12]);

            AddItem(Power);
        }

        // L키를 누르면 가방에 유기물탄환 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F4))
        {

            OrganicTan organic = (OrganicTan)Instantiate(items[13]);

            AddItem(organic);
        }

        // L키를 누르면 가방에 녹아버린 갑옷 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F5))
        {

            MeltedArmor Armor = (MeltedArmor)Instantiate(items[14]);

            AddItem(Armor);
        }

        // L키를 누르면 가방에 정밀탄환 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F6))
        {

            Precise precise = (Precise)Instantiate(items[15]);

            AddItem(precise);
        }

        // L키를 누르면 가방에 단백질흡수 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F7))
        {

            ProteinAbsortion absortion = (ProteinAbsortion)Instantiate(items[16]);

            AddItem(absortion);
        }

        // L키를 누르면 가방에 양자배터리 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F8))
        {

            QuantumBattery Battery = (QuantumBattery)Instantiate(items[17]);

            AddItem(Battery);
        }

        // L키를 누르면 가방에 퀵드로우탄창 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F9))
        {

            QuickTan tan = (QuickTan)Instantiate(items[18]);

            AddItem(tan);
        }

        // L키를 누르면 가방에 레이저저출력 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F10))
        {

            Raiserlightweight weight = (Raiserlightweight)Instantiate(items[19]);

            AddItem(weight);
        }

        // L키를 누르면 가방에 스파게티 알고리즘 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F11))
        {

            SpaghettiAlgorism algorism = (SpaghettiAlgorism)Instantiate(items[20]);

            AddItem(algorism);
        }

        // L키를 누르면 가방에 합금 장갑 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F12))
        {

           SpecialGloves gloves = (SpecialGloves)Instantiate(items[21]);

            AddItem(gloves);
        }

        // L키를 누르면 가방에 도둑의 장갑 아이템을 넣는다.
        if (Input.GetKeyDown(KeyCode.F3))
        {

            TheifsGloves gloves = (TheifsGloves)Instantiate(items[22]);

            AddItem(gloves);
        }

        */
    }




    public void AddBag(Bag bag)
    {
        // 빈 가방을 찾아서 등록한다.
        foreach (BagButton bagbutton in bagButtons)
        {
            if (bagbutton.MyBag == null)
            {
                bagbutton.MyBag = bag;
                bags.Add(bag);
                break;
            }
        }
    }

    public void AddItem(Items item)
    {

        foreach (Bag bag in bags)
        {
            // 가방 리스트 중에 빈슬롯 이 있는
            // 가방을 찾고 해당 가방에 아이템을 추가합니다.
            if (bag.MyBagScript.AddItem(item))
            {
                return;
            }
        }

    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //씬 이름 체크(던전일 때는 유물 로드)
        if (scene.name == "Dungeon")
        {

        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
