using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunAnimator : MonoBehaviour
{
	public SpriteAnimation Animation;
	private Image _image;
	public bool Playing = false;

	void Start()
	{
		_image = GetComponent<Image>();
		_image.sprite = Animation.frames[0].sprite;
	}
	public void Shoot()
	{
		if (Playing == true)
		{
			return;
		}

		Playing = true;
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

		Playing = false;
	}
}
