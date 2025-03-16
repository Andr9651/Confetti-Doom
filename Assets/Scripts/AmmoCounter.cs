using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{

	private TMP_Text _text;
	private Weapon _weapon;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_text = GetComponent<TMP_Text>();
		_weapon = GetComponentInParent<Weapon>();
		print("dwad" + _weapon.CurrentAmmo);
	}

	// Update is called once per frame
	void Update()
	{
		_text.text = _weapon.CurrentAmmo.ToString();
	}
}
