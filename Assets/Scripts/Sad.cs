
using UnityEngine;

public class Sad : MonoBehaviour
{
	[field: SerializeField]
	public Sprite SadMaterial { get; set; }
	[field: SerializeField]
	public Sprite HappyMaterial { get; set; }
	// Start is called once before the first execution of Update after the MonoBehaviour is created

	public Game Game { get; set; }
	void Start()
	{
		this.GetComponent<SpriteRenderer>().sprite = SadMaterial;
		Game = GameObject.FindWithTag("Game").GetComponent<Game>();
	}

	public void MakeHappy()
	{
		GetComponent<SpriteRenderer>().sprite = HappyMaterial;
		gameObject.layer &= ~(1 << 8);
		tag = "Happy";
		Game.CountHappy();
	}
}
