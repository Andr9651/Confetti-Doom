
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClownFace : MonoBehaviour
{
	private Image _faceImage;
	private Game _game;
	public List<Sprite> faces;
	public int faceIndex;

	void Start()
	{
		_faceImage = GetComponent<Image>();
		_game = GetComponentInParent<Game>();
		_faceImage.sprite = faces[0];
	}

	void Update()
	{
		int newFaceIndex = Mathf.FloorToInt(Helpers.MapRange(_game.HappyPercent, 0, 1, 0, faces.Count - 1));

		if (faceIndex != newFaceIndex)
		{
			faceIndex = newFaceIndex;
			_faceImage.sprite = faces[faceIndex];
		}
	}
}
