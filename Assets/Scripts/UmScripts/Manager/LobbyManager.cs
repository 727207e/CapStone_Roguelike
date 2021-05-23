using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public GameObject Screen_GameTitle;
    public GameObject Screen_LoadGame;
    public GameObject Screen_NewGame;
    public GameObject Screen_Option;

    public GameObject clickToContinue;
    public GameObject Screen_SelectMode;

    bool Continue = false;

    Coroutine coroutine_Blink;


    // Start is called before the first frame update
    void Start()
    {
        ScreenActive_MakeFalse();
        Screen_GameTitle.SetActive(true);


        coroutine_Blink = StartCoroutine(Blink()); // press any button 깜박임


    }

    // Update is called once per frame
    void Update()
    {
        //아직 아무키도 못눌렀다면
        if (!Continue)
        {
            //아무키나 누르면 진행
            if (Input.anyKey)
            {
                Screen_GameTitle.SetActive(false);
                StopCoroutine(coroutine_Blink);
                Continue = true;
                Screen_SelectMode.SetActive(true);

            }
        }

    }

    void ScreenActive_MakeFalse()
    {
        Screen_LoadGame.SetActive(false);
        Screen_NewGame.SetActive(false);
        Screen_Option.SetActive(false);

        Screen_SelectMode.SetActive(false);


        clickToContinue.SetActive(false);
    }

    IEnumerator Blink()
    {
        while (!Continue)
        {
            clickToContinue.SetActive(true);
            yield return new WaitForSeconds(1f);
            clickToContinue.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }

    public void MoveScene_ToStroyTelling()
    {
        SceneManager.LoadScene("2_StroyTelling");
    }

    public void BTN_BackToMenu()
    {
        ScreenActive_MakeFalse();
        Screen_SelectMode.SetActive(true);
    }

    public void BTN_NewGame()
    {
        MoveScene_ToStroyTelling();
    }

    public void BTN_LoadGame()
    {
        ScreenActive_MakeFalse();
        Screen_LoadGame.SetActive(true);
    }

    public void BTN_Option()
    {
        ScreenActive_MakeFalse();
        Screen_Option.SetActive(true);
    }

    public void BTN_Quit()
    {

    }
}
