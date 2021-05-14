using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour
{
	//음악 담아둘 행렬
	public SoundGroup[] soundGroups;


	//구조체
	[System.Serializable]
	public class SoundGroup
	{
		public string groupID;
		public AudioClip[] group;
	}

	//딕셔너리를 활용하여 음악을 찾아온다.
	Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>();

	void Awake()
	{
		//음악 담아둔 행렬을 사전에 등록
		foreach (SoundGroup soundGroup in soundGroups)
		{
			groupDictionary.Add(soundGroup.groupID, soundGroup.group);
		}
	}

	public AudioClip GetClipFromName(string name)
	{
		//Containskey를 활용하여 name을 찾아냅니다. (있으면 true, 없으면 false)
		if (groupDictionary.ContainsKey(name))
		{
			AudioClip[] sounds = groupDictionary[name];
			return sounds[Random.Range(0, sounds.Length)];
		}
		return null;
	}


}
