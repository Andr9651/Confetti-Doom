using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
	public int SadPeopleLeft;
	public int MaxSadPeople;
	public float HappyPercent => 1f - (SadPeopleLeft / (float)MaxSadPeople);

	public float TimeLimit = 30;
	private float _startTime;
	public float RemainingTime => _startTime + TimeLimit - Time.time;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		SadPeopleLeft = GameObject.FindGameObjectsWithTag("victim").Length;
		MaxSadPeople = SadPeopleLeft;
		print(SadPeopleLeft);
		_startTime = Time.time;
	}

	void Update()
	{
		if (RemainingTime <= 0)
		{
			print("You Lose");
		}
	}

	public void CountHappy()
	{
		SadPeopleLeft--;

		if (SadPeopleLeft == 0)
		{
			print("You Won");
		}
	}
}
