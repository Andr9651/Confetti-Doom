using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UIElements;
using System.Collections;
[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
	public PopperType type = PopperType.Popper;

	[Foldout("Particle Settings")]
	public int ConfettiAmount = 100;

	[Foldout("Particle Settings")]
	public float ConfettiSpeed = 30;

	[Foldout("Particle Settings")]
	public float ConfettiAngle = 15;


	[Foldout("Hit detection")]
	public bool Penetrating = false;

	[Foldout("Hit detection")]
	public float Range = 3;

	[Foldout("Hit detection")]
	public float Size = 3;


	[Foldout("Animation")]
	public SpriteAnimation Animation;

	[Foldout("Animation")]
	public int NoAmmoFrame;

	[Foldout("Sound")]
	public AudioClip ShootSound;

	[Foldout("Ammo")]
	public Sprite AmmoSprite;

	[Foldout("Ammo")]
	public int ammoPickupAmount = 1;


	[Foldout("Drop")]
	public Rigidbody dropPrefab;

	[Foldout("Drop")]
	public float dropTime;

	[Foldout("Drop")]
	public float dropSpeed = 5;

	[Foldout("Drop")]
	public float dropDistance = 1.5f;
	private RaycastHit[] collisions = new RaycastHit[50];

	public void HitVictims(Vector3 startPos, Vector3 direction)
	{
		var position = startPos + direction * Size;
		Debug.DrawLine(position, position + direction.normalized * Range, Color.red, 3);

		if (Penetrating == true)
		{
			var hits = Physics.SphereCastAll(position, Size, direction, Range, 1 << 8);

			foreach (var hit in hits)
			{
				Debug.Log($"print: {hit.collider.gameObject.name}");
				hit.collider.gameObject.GetComponent<Sad>().MakeHappy();

			}
		}
		else
		{
			if (Physics.SphereCast(position, Size, direction, out RaycastHit hit, Range, 1 << 8))
			{
				Debug.Log($"print: {hit.collider.gameObject.name}");

				hit.collider.gameObject.GetComponent<Sad>().MakeHappy();
			}
		}
	}

	public IEnumerator Drop(Transform transform)
	{
		yield return new WaitForSeconds(dropTime);
		var pipe = Instantiate(dropPrefab, transform.position + transform.forward * dropDistance, Random.rotation);
		pipe.GetComponent<Rigidbody>().AddForce(transform.forward * dropSpeed, ForceMode.Impulse);
	}
}
