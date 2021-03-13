using UnityEngine;

public class Follow_Attack : MonoBehaviour
{
  [Header("Follow")]
  [SerializeField] byte followSpeed;
  [SerializeField] byte followRange;
  [SerializeField] byte attackRange;
  [SerializeField] byte stopDistance;
  [SerializeField] byte retriveDistance;
  [SerializeField] Health_Death enemyHealthCurrentObj;
  private Player player;
  private float distanseBetwwenEnemyAndPlayer;
  private Health_Dye_Trigger playerDye;

  [Header("Attack")]
  [SerializeField] GameObject bullets;
  [SerializeField] Transform attackPoint;
  [SerializeField] float startTimeBetweenShot;
  [SerializeField] int bulletForce = 800;
  [SerializeField] float impactForce = 500f;
  private RaycastHit hit;
  private GameObject currentBullet;
  private float timeBetweenShot;

  [Header("Refernces")]
  public bool isPetroling;

  private void Start()
  {
    playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    
    timeBetweenShot = startTimeBetweenShot;
  }   

  private void Update()
  { 
     if(playerDye.isPlayerAlive)
     {
       // Distanse between Enemy and player.
       distanseBetwwenEnemyAndPlayer = Vector3.Distance(transform.position,player.transform.position);

       Follow();
       Attack();
     }
  }

   private void Follow()
   {
    if(((distanseBetwwenEnemyAndPlayer<followRange) || enemyHealthCurrentObj.isEnemyHit) && (distanseBetwwenEnemyAndPlayer>stopDistance))
    {
      transform.position = Vector3.MoveTowards(transform.position,player.transform.position,(followSpeed * Time.deltaTime));  // Follow to player.
      isPetroling = false;
    }
    else if((distanseBetwwenEnemyAndPlayer<followRange) && (distanseBetwwenEnemyAndPlayer<=stopDistance) && (distanseBetwwenEnemyAndPlayer>=retriveDistance))
    {
      transform.position = this.transform.position; // Stop Enemy.
      isPetroling = false;
    }
    else if((distanseBetwwenEnemyAndPlayer<followRange) && (distanseBetwwenEnemyAndPlayer<followRange))
    {
      transform.position = Vector3.MoveTowards(transform.position,player.transform.position,(-followSpeed * Time.deltaTime)); // Reverse Back Enemy.
      isPetroling = false;
    }
    else if(distanseBetwwenEnemyAndPlayer>followRange)
    {
      isPetroling = true; 
    }
   }

   private void Attack()
   {
      Vector3 target = player.transform.position - transform.position;
      if(distanseBetwwenEnemyAndPlayer<=attackRange)
     { 
        isPetroling = false;
        if(timeBetweenShot<=0)
        {
         currentBullet = Instantiate(bullets,attackPoint.position,Quaternion.identity); // Shoot
         transform.forward = target.normalized;
         currentBullet.GetComponent<Rigidbody>().AddForce(target.normalized * bulletForce,ForceMode.Impulse);
         Physics.Raycast(transform.position,target,out hit);
        if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
         timeBetweenShot = startTimeBetweenShot;
        }
        else
        {
           timeBetweenShot -= Time.deltaTime;
        }

        Destroy(currentBullet,3f);
     }
   }
     
   
    
}
