using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
	[SerializeField]
	public List<WeaponData> weapons;
	private int weaponIndex;
	private WeaponData currentWeapon => weapons[weaponIndex];
	private ParticleSystem _confetti;
	private GunAnimator _animator;
	private Coroutine _animationRoutine;
	private Transform _camera;
	private AudioSource _audio;

	private Dictionary<PopperType, int> _ammo;

	public int CurrentAmmo
	{
		get => _ammo[currentWeapon.type];
		set => _ammo[currentWeapon.type] = value;
	}

	void Awake()
	{

		_ammo = new Dictionary<PopperType, int>
		{
			[PopperType.Popper] = 8,
			[PopperType.DoublePopper] = 0,
			[PopperType.Cannon] = 0,
		};

	}

	void Start()
	{

		_confetti = GetComponentInChildren<ParticleSystem>();
		_animator = GetComponentInChildren<GunAnimator>();
		_audio = GetComponent<AudioSource>();
		_camera = Camera.main.transform;
	}

	public void ChangeWeapon(WeaponData newWeapon)
	{
		weaponIndex = weapons.FindIndex((element) => element.type == newWeapon.type);
		_animator.SetAnimation(newWeapon.Animation, CurrentAmmo);
		if (_animationRoutine != null)
		{
			StopCoroutine(_animationRoutine);
		}
		_animator.Playing = false;
	}

	private void OnScrollWheel(InputValue value)
	{
		var input = value.Get<Vector2>().y;
		print((int)Mathf.Clamp(input, -1, 1));
		weaponIndex += (int)Mathf.Clamp(input, -1, 1);

		if (weaponIndex == weapons.Count)
		{
			weaponIndex -= weapons.Count;
		}

		if (weaponIndex == -1)
		{
			weaponIndex += weapons.Count;
		}

		ChangeWeapon(currentWeapon);
	}

	private void OnNext()
	{
		weaponIndex++;

		if (weaponIndex == weapons.Count)
		{
			weaponIndex -= weapons.Count;
		}

		ChangeWeapon(currentWeapon);
	}

	private void OnPrevious()
	{
		weaponIndex--;

		if (weaponIndex == -1)
		{
			weaponIndex += weapons.Count;
		}

		ChangeWeapon(currentWeapon);

	}

	private void OnAttack()
	{
		if (CurrentAmmo <= 0)
		{
			return;
		}

		if (_animator.Playing == true)
		{
			return;
		}

		_animator.Playing = true;
		CurrentAmmo--;

		currentWeapon.HitVictims(_camera.position, _camera.forward);

		if (currentWeapon.dropPrefab != null)
		{
			StartCoroutine(currentWeapon.Drop(_camera));
		}

		_audio.PlayOneShot(currentWeapon.ShootSound);

		_animationRoutine = StartCoroutine(_animator.PlayAnimation(currentWeapon.Animation, CurrentAmmo));

		var main = _confetti.main;
		main.startSpeed = new ParticleSystem.MinMaxCurve(3f, currentWeapon.ConfettiSpeed);

		var shape = _confetti.shape;
		shape.angle = currentWeapon.ConfettiAngle;

		_confetti.Emit(currentWeapon.ConfettiAmount);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.CompareTag("Ammo"))
		{
			AmmoPickup ammo = hit.gameObject.GetComponent<AmmoPickup>();
			if (ammo.IsPickedUp == true)
			{
				return;
			}

			ammo.IsPickedUp = true;
			_ammo[ammo.WeaponType.type] += ammo.WeaponType.ammoPickupAmount;
			Destroy(hit.gameObject);

			if (_animator.Playing == false || ammo.WeaponType.type != currentWeapon.type)
			{
				weaponIndex = weapons.IndexOf(ammo.WeaponType);
				ChangeWeapon(currentWeapon);
			}

		}
	}
}
