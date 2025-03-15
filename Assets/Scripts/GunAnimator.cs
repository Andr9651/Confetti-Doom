using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunAnimator : MonoBehaviour
{
	public SpriteAnimation Animation;
	private Image _image;
	private bool _playing = false;

	void Start()
	{
		_image = GetComponent<Image>();
		_image.sprite = Animation.frames[0].sprite;
	}
	public void Shoot()
	{
		if (_playing == true)
		{
			return;
		}

		_playing = true;
		StartCoroutine(PlayAnimation());
	}

	private IEnumerator PlayAnimation()
	{
		foreach (var frame in Animation.frames)
		{
			_image.sprite = frame.sprite;
			yield return new WaitForSeconds(frame.time);
		}

		_image.sprite = Animation.frames[0].sprite;

		_playing = false;
	}
}
