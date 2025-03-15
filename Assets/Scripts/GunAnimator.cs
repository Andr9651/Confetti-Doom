using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunAnimator : MonoBehaviour
{
	public SpriteAnimation Animation;
	public Image Image;
	public bool Playing = false;

	void Start()
	{
		Image = GetComponent<Image>();
		Image.sprite = Animation.frames[0].sprite;
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
			Image.sprite = frame.sprite;
			yield return new WaitForSeconds(frame.time);
		}

		Image.sprite = Animation.frames[0].sprite;

		Playing = false;
	}
}
