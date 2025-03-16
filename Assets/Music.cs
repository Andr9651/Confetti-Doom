using UnityEngine;


public class Music : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		var musics = FindObjectsByType<Music>(FindObjectsSortMode.None);
		foreach (var music in musics)
		{
			if (music != this)
			{
				Destroy(gameObject);
			}
		}

		DontDestroyOnLoad(gameObject);
	}
}
