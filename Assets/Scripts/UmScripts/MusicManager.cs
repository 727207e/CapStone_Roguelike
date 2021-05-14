using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

	public AudioClip mainTheme;
	public AudioClip menuTheme;

	void Start()
	{
		AudioManager.instance.PlayMusic(menuTheme, 2);
	}

	void Update()
	{
	}
}
