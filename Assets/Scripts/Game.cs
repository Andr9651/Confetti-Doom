using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Input = UnityEngine.Windows.Input;

public class Game : MonoBehaviour
{
	public int SadPeopleLeft;
	public int MaxSadPeople;
	public float HappyPercent => 1f - (SadPeopleLeft / (float)MaxSadPeople);

	public float TimeLimit = 30;
	private float _startTime;
	private bool Lost { get; set; }
	public bool TimerEnabled;
	public float RemainingTime => _startTime + TimeLimit - Time.time;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		SadPeopleLeft = GameObject.FindGameObjectsWithTag("victim").Length;
		MaxSadPeople = SadPeopleLeft;
		print(SadPeopleLeft);
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			_startTime = Time.time;
			TimerEnabled = true;
		}
		
	}

	void OnRestart()
	{
		print("Restart");
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
	void Update()
	{
		
		if (RemainingTime <= 0 && TimerEnabled  )
		{
			Lost = true;
			GameObject.FindWithTag("LoseScreen").GetComponent<Image>().enabled = true;
			print("You Lose");
		}
	}

	public void CountHappy()
	{
		SadPeopleLeft--;

		if (SadPeopleLeft == 0 && !Lost)
		{
			int currentLevel = SceneManager.GetActiveScene().buildIndex;
			int nextlevel = currentLevel + 1;
			print(currentLevel);
			print(nextlevel);
			if (nextlevel == SceneManager.sceneCountInBuildSettings )
			{
				print("No more Game");
				GameObject.FindWithTag("WinScreen").GetComponent<Image>().enabled = true;
			}
			else
			{
			SceneManager.LoadScene(nextlevel, LoadSceneMode.Single);
            			print("You Won");	
			}
			
		}
	}
}
