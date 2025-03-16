using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
	private Transform _camera;
	public float walkSpeed = 6f;
	public float jumpPower = 0f;
	public float gravity = 10f;

	public float lookSpeed = 2f;
	public float lookXLimit = 45f;

	Vector3 moveDirection = Vector3.zero;
	float rotationX = 0;

	public bool canMove = true;

	private Vector2 moveInput;
	private Vector2 lookInput;


	CharacterController characterController;
	void Start()
	{
		_camera = GetComponentInChildren<Camera>().transform;

		characterController = GetComponent<CharacterController>();
		SetCursorLock(true);
	}

	void Update()
	{

		#region Handles Movment
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);

		float curSpeedX = canMove ? walkSpeed * moveInput.y : 0;
		float curSpeedY = canMove ? walkSpeed * moveInput.x : 0;
		float movementDirectionY = moveDirection.y;
		moveDirection = (forward * curSpeedX) + (right * curSpeedY);

		#endregion

		moveDirection.y = movementDirectionY;

		if (!characterController.isGrounded)
		{
			moveDirection.y -= gravity * Time.deltaTime;
		}

		#region Handles Rotation
		characterController.Move(moveDirection * Time.deltaTime);

		if (canMove)
		{
			rotationX += -lookInput.y * lookSpeed;
			rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
			_camera.localRotation = Quaternion.Euler(rotationX, 0, 0);
			transform.rotation *= Quaternion.Euler(0, lookInput.x * lookSpeed, 0);
		}

		#endregion
	}

	private void OnLook(InputValue value)
	{
		lookInput = value.Get<Vector2>();
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
}