using System.Collections;

public static class Utility
{

	public static T[] ShuffleArray<T>(T[] array, int seed)
	{
		System.Random prng = new System.Random(seed);

		//fisher-yates shuffles
		//기준을 제일 앞에있는 0번을 기준으로 0변이 갈 번호를 찾는다.
		//예를들어, 0번이 3위치로 이동시키면 3위치에 있는 변수가 0번위치로 간다.
		//이후, 3위치에 있는 0번이 다른 2위치로 이동한다.
		//그러면 2위치에 있는 변수가 3위치로 이동한다. 0번은 2위치로 이동한다. 이런식으로 무작위로 셔플하게 된다.

		for (int i = 0; i < array.Length - 1; i++)
		{
			int randomIndex = prng.Next(i, array.Length);
			T tempItem = array[randomIndex];
			array[randomIndex] = array[i];
			array[i] = tempItem;
		}

		return array;

		//주석주석




	}
}
