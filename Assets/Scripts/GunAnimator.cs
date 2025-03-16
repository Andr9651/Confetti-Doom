using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GunAnimator : MonoBehaviour
{
	public SpriteAnimation Animation;
	public Image Image;
	public bool Playing = false;
	public bool ammoLeft = true;
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

	public void SetAnimation(SpriteAnimation animation, int ammo)
	{
		ammoLeft = ammo > 0;

		Animation = animation;

		if (ammoLeft)
		{
			Image.sprite = Animation.frames[0].sprite;
		}
		else
		{
			Image.sprite = Animation.frames.Last().sprite;
		}
	}

	public IEnumerator PlayAnimation(SpriteAnimation animation, int ammo)
	{
		Animation = animation;
		ammoLeft = ammo > 0;
		return PlayAnimation();
	}

	public IEnumerator PlayAnimation()
	{
		foreach (var frame in Animation.frames)
		{
			Image.sprite = frame.sprite;
			yield return new WaitForSeconds(frame.time);
		}

		if (ammoLeft)
		{
			Image.sprite = Animation.frames[0].sprite;
		}
		else
		{
			Image.sprite = Animation.frames.Last().sprite;
			print("No Ammo");
		}


		Playing = false;
	}
}
