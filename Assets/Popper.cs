using Unity.VisualScripting;
using UnityEngine;

public class Popper : MonoBehaviour
{ [field: SerializeField]
    public GameObject ConfettiBase { get; set; }
    void FixedUpdate()
        {

            if (Input.GetMouseButtonDown(0))
            {


                for (int i = 0; i < Random.Range(3,10); i++)
                {
                    Makeconfetti();
                }

                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, fwd, out hit, 3))
                {
                    print($"print: {hit.collider.gameObject.name}");
                    if (hit.collider.gameObject.CompareTag("victim"))
                    {
                        hit.collider.gameObject.GetComponent<Sad>().MakeHappy();

                    }

                }
                else
                {
                    print("No Object");
                }
            }
        }

    void Makeconfetti()
    {
        var position = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y,
            transform.position.z + Random.Range(-2f, 2f));

   
       var newObject =  Instantiate(ConfettiBase, position,   Random.rotation);
       newObject.GetComponent<MeshRenderer>().material.color = GetRandomColor(); 
    }

    Color GetRandomColor()
    {
        switch (Random.Range(1,6))
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
