using UnityEngine;

public class Billboard : MonoBehaviour
{
	private Transform _camera;
	private Transform _transform;

	void Start()
	{
		_camera = Camera.main.transform;
		_transform = transform;
	}

	void Update()
	{
		var thisPos = _transform.position;
		var cameraPos = _camera.position;
		thisPos.y = 0;
		cameraPos.y = 0;
		_transform.forward = thisPos - cameraPos;
	}
}
