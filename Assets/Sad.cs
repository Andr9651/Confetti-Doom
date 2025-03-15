using Unity.VisualScripting;
using UnityEngine;

public class Sad : MonoBehaviour
{
    [field: SerializeField]
    public Texture2D SadMaterial { get; set; }
    [field: SerializeField]
    public Texture2D HappyMaterial { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [field: SerializeField]
    public GameObject Game { get; set; }
    void Start()
    {
        this.GetComponent<MeshRenderer>().material.mainTexture = SadMaterial;
        
    }

    public void MakeHappy()
    {
        this.GetComponent<MeshRenderer>().material.mainTexture = HappyMaterial;
        this.gameObject.tag = "Happy";
        Game.GetComponent<Game>().SadPeopleLeft--;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
