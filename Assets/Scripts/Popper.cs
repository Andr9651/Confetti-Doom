using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Popper : MonoBehaviour
{
	[field: SerializeField]
	public GameObject ConfettiBase { get; set; }
	[field: SerializeField]
	public GameObject CannonEmtpy { get; set; }
	private GunAnimator _gunAnimator;
	[field: SerializeField]
	public SpriteAnimation PopperAnimation { get; set; }
	[field: SerializeField]
	public SpriteAnimation DoublePopperAnimation { get; set; }
	[field: SerializeField]
	public SpriteAnimation CannonAnimation { get; set; }
	float NextShot { get; set; }
	bool Reloading { get; set; }
	float Speed { get; set; }
	float MinAmount { get; set; }
	float MaxAmount { get; set; }

	float MinSpread { get; set; }
	float MaxSpread { get; set; }
	float Distance { get; set; }
	float ReloadSeconds { get; set; }

	public PopperType CurrentType { get; set; }

	public int PopperAmmo { get; set; }
	public int DoublePopperAmmo { get; set; }
	public int CannonAmmo { get; set; }

	public enum PopperType
	{
		Popper,
		DoublePopper,
		Cannon
	}

	public void AddAmmo(PopperType Type)
	{
		var curentAmmo = 0;
		var oldcount = 0;
		switch (Type)
		{
			case PopperType.DoublePopper:

				oldcount = DoublePopperAmmo;
				DoublePopperAmmo = DoublePopperAmmo + 2;
				curentAmmo = DoublePopperAmmo;
				break;
			case PopperType.Cannon:
				oldcount = CannonAmmo;
				CannonAmmo = CannonAmmo + 1;
				curentAmmo = CannonAmmo;
				break;
			default:
				oldcount = PopperAmmo;
				PopperAmmo = PopperAmmo + 8;
				curentAmmo = PopperAmmo;

				break;
		}

		if (CurrentType != Type || oldcount == 0)
		{
			PickPopper(Type);
		}
	}
	void PickPopper(PopperType Type)
	{
		NextShot = Time.time;
		var CurrentAmmo = 0;
		switch (Type)
		{
			case PopperType.DoublePopper:

				_gunAnimator.Animation = DoublePopperAnimation;
				CurrentAmmo = DoublePopperAmmo;
				MaxAmount = 20;
				MinAmount = 15;
				Speed = 70;
				MinSpread = -1.5f;
				MaxSpread = 1.5f;
				CurrentType = PopperType.DoublePopper;
				Distance = 8;
				ReloadSeconds = 1.3f;
				break;
			case PopperType.Cannon:
				_gunAnimator.Animation = CannonAnimation;
				CurrentAmmo = CannonAmmo;
				MaxAmount = 300;
				MinAmount = 250;
				Speed = 160;
				MinSpread = -3f;
				MaxSpread = 3f;
				CurrentType = PopperType.Cannon;
				Distance = 20;
				ReloadSeconds = 3f;
				break;
			default:
				_gunAnimator.Animation = PopperAnimation;
				MaxAmount = 5;
				MinAmount = 3;
				Speed = 30;
				MinSpread = -0.5f;
				MaxSpread = 0.5f;
				CurrentType = PopperType.Popper;
				Distance = 4;
				ReloadSeconds = 1;
				CurrentAmmo = PopperAmmo;
				break;
		}

		if (CurrentAmmo == 0)
		{
			_gunAnimator.ammoLeft = false;

			_gunAnimator.Image.sprite = _gunAnimator.Animation.frames.Last().sprite;

		}
		else
		{
			_gunAnimator.ammoLeft = true;
			_gunAnimator.Image.sprite = _gunAnimator.Animation.frames[0].sprite;
		}

	}
	bool addingAmmo = false;
	void OnControllerColliderHit(ControllerColliderHit hit)
	{



		if (hit.gameObject.CompareTag("Ammo"))
		{
			Ammo ammo = hit.gameObject.GetComponent<Ammo>();
			if (!ammo.IsPickedUp)
			{
				ammo.IsPickedUp = true;
				addingAmmo = true;
				AddAmmo(ammo.Type);
				Debug.Log("Got Ammo");
				Destroy(hit.gameObject);
				addingAmmo = false;
			}

		}
	}

	void OnAttack()
	{
		if (_gunAnimator.Playing || !_gunAnimator.ammoLeft)
		{
			return;
		}

		Makeconfetti();

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, fwd, out hit, Distance))
		{
			print($"print: {hit.collider.gameObject.name}");
			if (hit.collider.gameObject.CompareTag("victim"))
			{
				hit.collider.gameObject.GetComponent<Sad>().MakeHappy();

			}

		}

	}

	void Makeconfetti()
	{
		_gunAnimator.Shoot();
		for (int i = 0; i < Random.Range(MinAmount, MaxAmount); i++)
		{
			var position = new Vector3(transform.position.x + Random.Range(MinSpread, MaxSpread),
					transform.position.y + Random.Range(MinSpread, MaxSpread),
					transform.position.z + Random.Range(MinSpread, MaxSpread));


			var newObject = Instantiate(ConfettiBase, position, Random.rotation);
			newObject.GetComponent<Rigidbody>().AddForce(transform.forward * Speed, ForceMode.Impulse);
			newObject.GetComponent<MeshRenderer>().material.color = GetRandomColor();
		}

		Reload();
	}

	void Reload()
	{
		var currentAmmo = 0;
		Reloading = true;
		switch (CurrentType)
		{
			case PopperType.DoublePopper:
				DoublePopperAmmo--;
				currentAmmo = DoublePopperAmmo;
				break;
			case PopperType.Cannon:
				CannonAmmo--;
				currentAmmo = CannonAmmo;
				break;
			default:
				PopperAmmo--;
				currentAmmo = PopperAmmo;
				break;
		}
		print(currentAmmo);
		if (currentAmmo == 0)
		{
			_gunAnimator.ammoLeft = false;
		}
		NextShot = Time.time + ReloadSeconds;
		if (CurrentType == PopperType.Cannon)
		{
			var pipe = Instantiate(CannonEmtpy, transform.position, Random.rotation);
			pipe.GetComponent<Rigidbody>().AddForce(transform.forward * 1, ForceMode.Impulse);
		}
	}
	void Reloaded()
	{


		Reloading = false;

	}
	Color GetRandomColor()
	{
		switch (Random.Range(1, 6))
		{

			case 1:
				return Color.blue;

			case 2:
				return Color.yellow;

			case 3:
				return Color.green;

			case 4:
				return Color.magenta;

			case 5:
				return Color.red;

			default:
				return Color.red;

		}
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{


		_gunAnimator = GetComponentInChildren<GunAnimator>();
		PickPopper(PopperType.Cannon);
	}

	// Update is called once per frame
	void Update()
	{
		if (NextShot < Time.time && Reloading)
		{
			Reloaded();
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			PickPopper(PopperType.Popper);
			print(PopperType.Popper);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			PickPopper(PopperType.DoublePopper);
			print(PopperType.DoublePopper);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			PickPopper(PopperType.Cannon);
			print(PopperType.Cannon);
		}
	}
}
