using UnityEngine;

public class Popper : MonoBehaviour
{
    void FixedUpdate()
        {

            if (Input.GetMouseButtonDown(0))
            {

                Debug.Log("Pressed left-click.");

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
            
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
