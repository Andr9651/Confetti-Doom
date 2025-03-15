using Unity.VisualScripting;
using UnityEngine;

public class Popper : MonoBehaviour
{ [field: SerializeField]
    public GameObject ConfettiBase { get; set; }
    private GunAnimator _gunAnimator;
    [field: SerializeField]
    public SpriteAnimation PopperAnimation { get; set; }
    [field: SerializeField]
    public SpriteAnimation DoublePopperAnimation { get; set; }
    float NextShot { get; set; }
 bool Reloading { get; set; }
     float Speed { get; set; }
     float MinAmount { get; set; }
     float MaxAmount { get; set; }
    
     float MinSpread { get; set; }
     float MaxSpread { get; set; }
     float Distance { get; set; }
     float ReloadSeconds { get; set; }

    public  PopperType  CurrentType { get; set; }

   public enum PopperType
    {
        Popper,
        DoublePopper,
        Cannon
    }  
    
    void PickPopper(PopperType Type)
    {
        NextShot = Time.time;
        switch (Type)
        {
            case PopperType.DoublePopper:

                _gunAnimator.Animation = DoublePopperAnimation;
                
                MaxAmount = 15;
                MinAmount = 8;
                Speed = 40;
                MinSpread = -1f;
                MaxSpread = 1f;
                CurrentType = PopperType.DoublePopper;
                Distance = 6;
                ReloadSeconds = 1.3f;
                break;
            case PopperType.Cannon:
                MaxAmount = 80;
                MinAmount = 20;
                Speed = 100;
                MinSpread = -1.5f;
                MaxSpread = 1.5f;
                CurrentType = PopperType.Cannon;
                Distance = 10;
                ReloadSeconds = 3f;
                break;
            default:
                _gunAnimator.Animation = PopperAnimation;
                MaxAmount = 5;
                MinAmount = 3;
                Speed = 20;
                MinSpread = -0.5f;
                MaxSpread = 0.5f;
                CurrentType = PopperType.Popper;
                Distance = 3;
                ReloadSeconds = 1;
            
                break;
        }
        _gunAnimator.Image.sprite = _gunAnimator.Animation.frames[0].sprite;
    }
    
    void FixedUpdate()
        {

            if (Input.GetMouseButtonDown(0))
            {


                if (_gunAnimator.Playing)
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
        }

    void Makeconfetti()
    {
        _gunAnimator.Shoot();
        for (int i = 0; i < Random.Range(MinAmount, MaxAmount); i++)
        {
            var position = new Vector3(transform.position.x + Random.Range(MinSpread, MaxSpread),
                transform.position.y + 1f,
                transform.position.z + Random.Range(MinSpread, MaxSpread));


            var newObject = Instantiate(ConfettiBase, position, Random.rotation);
            newObject.GetComponent<Rigidbody>().AddForce(transform.forward * Speed, ForceMode.Impulse);
            newObject.GetComponent<MeshRenderer>().material.color = GetRandomColor();
        }


        Reload();
    }

    void Reload()
    {

        Reloading = true;
        NextShot = Time.time + ReloadSeconds;
    }
    void Reloaded()
    {

        Reloading = false;
      
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
        _gunAnimator = GetComponentInChildren<GunAnimator>();
        PickPopper(PopperType.Popper);
    }

    // Update is called once per frame
    void Update()
    {
        if (NextShot < Time.time)
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
