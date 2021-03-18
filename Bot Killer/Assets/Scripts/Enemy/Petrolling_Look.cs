using UnityEngine;

public class Petrolling_Look : MonoBehaviour
{
  [Header("Petrolling")]
  [SerializeField] Transform movePoint;
  [SerializeField] int minX;
  [SerializeField] int maxX;
  [SerializeField] int minY;
  [SerializeField] int maxY;
  [SerializeField] int minZ;
  [SerializeField] int maxZ;
  [SerializeField] byte petrollingSpeed;
  [SerializeField] float startWaitTime;
  private float waitTime;
  [SerializeField] Follow_Attack follow_Attack;  
  private Health_Dye_Trigger playerDye;
  private Player player;

  private void Start()
  {
      playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

     waitTime = startWaitTime;
     // Setting move point first time.
     movePoint.position = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),Random.Range(minZ,maxZ));
     follow_Attack.isPetroling =true;
  }

  private void Update()
  {
     if(!playerDye.isPlayerAlive)
      {
       Invoke("StartPetrolling",3f);
      }

     if(!follow_Attack.isPetroling) Look();
     if(follow_Attack.isPetroling) StartPetrolling();
  }

   private void StartPetrolling()
    {
        // Setting Enemy Position .
        transform.position = Vector3.MoveTowards(transform.position,movePoint.position,(petrollingSpeed * Time.deltaTime));
        
        if(Vector3.Distance(transform.position,movePoint.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                 // Setting move point every time.
                  movePoint.position = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),Random.Range(minZ,maxZ));
                  waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

   private void Look()
   {
    Vector3 lookVector = player.transform.position - transform.position;
    lookVector.y = transform.position.y;
    Quaternion rot = Quaternion.LookRotation(lookVector);
    transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
   }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            transform.position = transform.position + new Vector3(0.5f, 2f, 0.5f);
        }
        if (collision.gameObject.tag == "Roof")
        {
            transform.position = transform.position + new Vector3(0.5f, -2f, 0.5f);
        }
        if (collision.gameObject.tag == "ZPE")
        {
            transform.position = transform.position + new Vector3(0f, 0f, 2f);
        }
        if(collision.gameObject.tag == "ZNE")
        {
            transform.position = transform.position + new Vector3(0f, 0f, -2f);
        }
        if(collision.gameObject.tag == "XEP")
        {
            transform.position = transform.position + new Vector3(2f, 0f, 0f);
        }
        if(collision.gameObject.tag == "XNE")
        {
            transform.position = transform.position + new Vector3(-2f, 0f, 0f);
        }
    }
}
