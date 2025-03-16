using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
	[field: SerializeField]
	public WeaponData WeaponType;
	public bool IsPickedUp { get; set; }

	void Start()
	{
		GetComponent<SpriteRenderer>().sprite = WeaponType.AmmoSprite;
	}
}
