using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	[TextArea(2, 15)]
	string How_To_Use_Sound = "";

	public enum AudioChannel { Master, Sfx, Music };

	public float masterVolumePercent { get; private set; }
	public float sfxVolumePercent { get; private set; }
	public float musicVolumePercent { get; private set; }

	AudioSource sfx2DSource;
	AudioSource[] musicSources;
	int activeMusicSourceIndex;

	public static AudioManager instance; //싱글톤

	SoundLibrary library; // 음악이 들어있는 라이브러리

	void Awake()
	{
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

			library = GetComponent<SoundLibrary>();

			musicSources = new AudioSource[2];
			for (int i = 0; i < 2; i++)
			{
				GameObject newMusicSource = new GameObject("Music source " + (i + 1));
				musicSources[i] = newMusicSource.AddComponent<AudioSource>();
				newMusicSource.transform.parent = transform;

				newMusicSource.GetComponent<AudioSource>().loop = true;
			}
			GameObject newSfx2Dsource = new GameObject("2D sfx source");
			sfx2DSource = newSfx2Dsource.AddComponent<AudioSource>();
			newSfx2Dsource.transform.parent = transform;

			//만약 AudioManager가 처음 등장했다면
			//기본 정보를 대입해 준다.
			masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
			sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
			musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
		}
	}

	public void SetVolume(float volumePercent, AudioChannel channel)
	{
		//넘겨받은 음악 채널의 종류를 확인한다.
		switch (channel)
		{
			case AudioChannel.Master:
				masterVolumePercent = volumePercent;
				break;
			case AudioChannel.Sfx:
				sfxVolumePercent = volumePercent;
				break;
			case AudioChannel.Music:
				musicVolumePercent = volumePercent;
				break;
		}

		//모든 소리를 마스터 볼륨의 영향을 받아야 한다.
		musicSources[0].volume = musicVolumePercent * masterVolumePercent;
		musicSources[1].volume = musicVolumePercent * masterVolumePercent;

		//변경된 음악 볼륨 값을 저장한다.
		PlayerPrefs.SetFloat("master vol", masterVolumePercent);
		PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
		PlayerPrefs.SetFloat("music vol", musicVolumePercent);
	}

	public void PlayMusic(AudioClip clip, float fadeDuration = 1)
	{
		activeMusicSourceIndex = 1 - activeMusicSourceIndex; //2개 밖에 없기 때문에
		musicSources[activeMusicSourceIndex].clip = clip;
		musicSources[activeMusicSourceIndex].Play();

		//노래 변경시 페이드 아웃, 새로운 노래 페이드 인
		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}

	public void PlaySound(AudioClip clip, Vector3 pos)
	{
		//클립으로 노래 재생
		if (clip != null)
		{
			AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
		}
	}
	public void PlaySound(string soundName, Vector3 pos)
	{
		//사운드 이름으로 재생
		PlaySound(library.GetClipFromName(soundName), pos);
	}

	public void PlaySound2D(string soundName)
	{
		//이팩트 소리 출력
		sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
	}


	IEnumerator AnimateMusicCrossfade(float duration)
	{
		float percent = 0;

		while (percent < 1)
		{
			percent += Time.deltaTime * 1 / duration;

			//현재 노래 서서히 줄이기
			musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
			
			//새로운 노래 서서히 늘리기
			musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
			
			yield return null;
		}
	}
}
