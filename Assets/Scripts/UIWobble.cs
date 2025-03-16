
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWobble : MonoBehaviour
{
	private Transform _transform;
	public float frequency = 2;
	public float angle = 10;

	[Range(0, 2 * Mathf.PI)]
	public float offset;

	void Start()
	{
		_transform = transform;
	}

	void Update()
	{
		_transform.eulerAngles = new Vector3(0, 0, Mathf.Sin((Time.time + offset) / frequency) * angle);
	}
}
