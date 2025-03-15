using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	public float mouseSensitivity = 1;

	private Transform _camera;
	private Transform _transform;
	private CharacterController _character;
	private GunAnimator _gunAnimator;
	private float xRotation = 0;
	private Vector2 moveInput;
	public float speed = 5;

	void Awake()
	{
		_camera = GetComponentInChildren<Camera>().transform;
		_character = GetComponent<CharacterController>();
		_transform = transform;
		_gunAnimator = GetComponentInChildren<GunAnimator>();
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
		var speedVector = speed * Time.deltaTime * (transform.forward * moveInput.y + transform.right * moveInput.x);
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
		_gunAnimator.Shoot();
	}
}