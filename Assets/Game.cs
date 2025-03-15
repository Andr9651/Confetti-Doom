using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int SadPeopleLeft { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SadPeopleLeft = GameObject.FindGameObjectsWithTag("victim").Length;
        print(SadPeopleLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (SadPeopleLeft == 0)
        {
            print("Won");
        }
    }
}
