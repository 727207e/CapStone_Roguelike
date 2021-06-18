using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

	public Slider[] volumeSliders; // 생성된 슬라이더(볼륨 크기)
	public Toggle[] resolutionToggles; // 생성된 토글(해상도)
	public Toggle fullscreenToggle; // 생성된 토글(전체화면)
	public int[] screenWidths; //사용될 화면 크기
	int activeScreenResIndex; //사용된 토글 넘버

	public static MenuManager instance; // 싱글톤

	//사용될 메뉴 옵션
	public GameObject menu;
	public GameObject youWin_Image;
	public GameObject youLose_image;

	//메뉴옵션 활성화 비활성화
	private bool menu_active = false;

	private void Awake()
	{
		//메뉴 비활성화
		menu.SetActive(false);

		//존재한다면 파괴
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			//싱글톤 등록
			instance = this;
			DontDestroyOnLoad(gameObject);

		}
	}

    private void Update()
    {
		//메뉴 활성화
        if (Input.GetKeyDown(KeyCode.Escape) && !menu_active)
        {
			//게임 일시정지
			Time.timeScale = 0;

			//메뉴바 활성화
			menu.SetActive(true);
			menu_active = true;


		}    

		//메뉴 비활성화
		else if(Input.GetKeyDown(KeyCode.Escape) && menu_active)
        {
			//게임 재개
			Time.timeScale = 1;

			//메뉴 비활성화
			menu.SetActive(false);
			menu_active = false;
		}
    }


    void Start()
	{
		//스크린 화면 조정 ( 크기 및 전체화면 측정 )
		activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
		bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;

		//슬라이더 값 호출 및 저장
		volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
		volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
		volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;

		//i번째와 actvieScreenResIndex가 일치한다면 그 i 번째의 토글만 on 된다.
		for (int i = 0; i < resolutionToggles.Length; i++)
		{
			resolutionToggles[i].isOn = i == activeScreenResIndex;
		}

		//전체화면 
		fullscreenToggle.isOn = isFullscreen;
	}

	public void SetScreenResolution(int i)
	{
		//선택된 화질 번째 확인
		if (resolutionToggles[i].isOn)
		{
			activeScreenResIndex = i;
			float aspectRatio = 16 / 9f;
			
			//선택된 화질
			Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);

			print(screenWidths[i]);

			//값 저장
			PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
			PlayerPrefs.Save();
		}
	}

	public void SetFullscreen(bool isFullscreen)
	{
		//전체화면을 선택된다면 화질 비활성화
		for (int i = 0; i < resolutionToggles.Length; i++)
		{
			resolutionToggles[i].interactable = !isFullscreen;
		}


		if (isFullscreen)
		{
			//전체화면으로 변경
			Resolution[] allResolutions = Screen.resolutions;
			Resolution maxResolution = allResolutions[allResolutions.Length - 1];
			Screen.SetResolution(maxResolution.width, maxResolution.height, true);
		}
		else
		{
			//선택되어있던 화질로 변경
			SetScreenResolution(activeScreenResIndex);
		}

		//값 저장
		PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
		PlayerPrefs.Save();
	}

	public void SetMasterVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
	}

	public void SetMusicVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
	}

	public void SetSfxVolume(float value)
	{
		AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
	}

	public void diePlayerContinue()
	{                
		//Ui on
		GameManager.Instance.player.transform.Find("MainPlayerCharacter").GetComponent<msPlayerControllerNew>()
			.OnOffAllUI(false);
		GameManager.Instance.NextSceneFadein("1_GameOpening");
		youLose_image.SetActive(false);
	}

	public void diePlayerQuit()
    {
		Application.Quit();
	}

	public void victoryPlayerContinue()
	{
		GameManager.Instance.NextSceneFadein("3_LabScene");
		youWin_Image.SetActive(false);
	}

	public void victoryPlayerQuit()
    {
		Application.Quit();
	}

	public void dieImageAppear()
	{
		AudioManager.instance.PlayMusic("BGMGameOver", 1);
		youLose_image.SetActive(true);
    }

	public void victoryImageAppear()
	{
		AudioManager.instance.PlayMusic("BGMGameClear",1);
		youWin_Image.SetActive(true);
    }

}
