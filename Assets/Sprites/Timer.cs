using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	private Image _image;
	private Game _game;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_image = GetComponent<Image>();
		_game = GetComponentInParent<Game>();
	}

	// Update is called once per frame
	void Update()
	{
		_image.fillAmount = _game.RemainingTime / _game.TimeLimit;
	}
}
