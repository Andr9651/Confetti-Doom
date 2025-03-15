using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	public float mouseSensitivity = 1;

	private Transform _camera;
	private Transform _transform;
	private CharacterController _character;
	private float xRotation = 0;
	private Vector2 moveInput;
	public float speed = 5;

	void Awake()
	{
		_camera = GetComponentInChildren<Camera>().transform;
		_character = GetComponent<CharacterController>();
		_transform = transform;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		var speedVector = speed * Time.fixedDeltaTime * (transform.forward * moveInput.y + transform.right * moveInput.x);
		_character.SimpleMove(speedVector);
	}

	private void OnLook(InputValue value)
	{
		var input = value.Get<Vector2>();
		input *= mouseSensitivity;

		var cameraRotation = _camera.rotation.eulerAngles;
		xRotation -= input.y;
		xRotation = Mathf.Clamp(xRotation, -90, 90);
		cameraRotation.x = xRotation;
		_camera.eulerAngles = cameraRotation;

		var rotation = _transform.rotation.eulerAngles;
		rotation.y += input.x;
		_transform.eulerAngles = rotation;
	}



	private void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
	}

}
