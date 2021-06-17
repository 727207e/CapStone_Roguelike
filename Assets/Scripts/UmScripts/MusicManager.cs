using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

	void Start()
	{
		AudioManager.instance.PlayMusic("BGMIntro", 2);
	}

	void Update()
	{

	}
}
 