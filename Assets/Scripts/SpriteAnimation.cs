using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimation", menuName = "Scriptable Objects/SpriteAnimation")]
public class SpriteAnimation : ScriptableObject
{
	public List<SpriteFrame> frames;
}
