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
	private Vector2 lookInput;
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
		SetCursorLock(true);
	}

	public void SetCursorLock(bool isLocked)
	{
		if (isLocked)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	// Update is called once per frame
	void Update()
	{
		var cameraRotation = _camera.rotation.eulerAngles;
		xRotation -= lookInput.y;
		xRotation = Mathf.Clamp(xRotation, -90, 90);
		cameraRotation.x = xRotation;
		_camera.eulerAngles = cameraRotation;

		var rotation = _transform.rotation.eulerAngles;
		rotation.y += lookInput.x;
		_transform.eulerAngles = rotation;

		var speedVector = speed * Time.deltaTime * (transform.forward * moveInput.y + transform.right * moveInput.x);
		_character.Move(speedVector);
	}

	private void OnLook(InputValue value)
	{
		lookInput = value.Get<Vector2>();
		lookInput *= mouseSensitivity;
	}

	private void OnCancel()
	{
		SetCursorLock(false);
	}

	private void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
	}

	private void OnAttack()
	{
		SetCursorLock(true);

	}
}