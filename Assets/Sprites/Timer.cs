using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	private Image _image;
	private Game _game;
	private bool _enabled;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_image = GetComponent<Image>();
		_game = GetComponentInParent<Game>();

	
		
			
		
	}

	// Update is called once per frame
	void Update()
	{
		_enabled = _game.TimerEnabled;
		_image.enabled = _enabled;
		if (_enabled)
		{
			_image.fillAmount = _game.RemainingTime / _game.TimeLimit;
		}
	
	}
}
