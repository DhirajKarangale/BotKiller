using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   [SerializeField] NavMeshAgent agent;
   [SerializeField] Transform player;
   [SerializeField] LayerMask whatIsGround,whatIsPlayer;

   // Petoriling
   [SerializeField] Vector3 walkPoint;
   private bool walkPointSet;
   [SerializeField] float walkPointRange;

   // Attacking
   [SerializeField] public float timeBetweenAttack;
   private bool alreadyAttack;
   public GameObject projectile;

   // States
   [SerializeField] float sightRange,attackRange;
   [SerializeField] bool playerInSightRange,playerInAttackRange;

   // Health 
   [SerializeField] int health;

   // Shotting
    [Header("Shotting")]
    private GameObject currentBullet;
    private Vector3 directionOfAttackAndHitPoint;
    private Vector3 targetPoint;
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] int shootForce;
    [SerializeField] int impactForce;
    [SerializeField] int damage;
    [SerializeField] float timeBetweenShotting;
    [SerializeField] GameObject bullets;
    [SerializeField] GameObject muzzelFlash;
    [SerializeField] Camera fpscamera;
    [SerializeField] Transform attackPoint;
    private bool allowInvoke = true;

   private void Awake()
   {
       player = GameObject.Find("FPS Player").transform;
       agent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
       playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
       playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

       if(!playerInSightRange && !playerInAttackRange) Petoriling();
       if(playerInSightRange && !playerInAttackRange) ChasePlayer();
       if(playerInSightRange && playerInAttackRange) AttackPlayer();
   }

   private void Petoriling()
   {
      if(!walkPointSet) SearchWalkPoint();

      if(walkPointSet) 
      agent.SetDestination(walkPoint);

      Vector3 distanceToWalkPoint = transform.position - walkPoint;   
      
      // WalkPoint Reached
      if(distanceToWalkPoint.magnitude < 1f)
      walkPointSet = false;
   }

   private void SearchWalkPoint()
   {
       float randomX = Random.Range(-walkPointRange,walkPointRange);
       float randomZ = Random.Range(-walkPointRange,walkPointRange);

       walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);

       if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround)) 
       walkPointSet = true;
   }
   
    private void ChasePlayer()
   {
     agent.SetDestination(player.position);
   }

    private void AttackPlayer()
   {
       transform.LookAt(player);
     // Make Sure enemy dosent move
     agent.SetDestination(transform.position);
    

     if(!alreadyAttack)
     {
         // Attack Code here,
          Shoot();
           


        alreadyAttack =true;
        Invoke("ResetAttack",timeBetweenAttack);
     }
   }

   private void ResetAttack()
   {
     alreadyAttack = false;
   }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health<=damage)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrowGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }

   private void Shoot()
   {
  


        
        // Instantiate bullets.
        currentBullet = Instantiate(bullets, attackPoint.position, Quaternion.identity);

        // Rotate Bullets to Shoot Direction.
       // currentBullet.transform.forward = directionOfAttackAndHitPoint.normalized;

        // Add force to bullet.
        currentBullet.GetComponent<Rigidbody>().AddForce(attackPoint.position.normalized * shootForce, ForceMode.Impulse);

        // Destroy Bullets Automatically. 
        Destroy(currentBullet, 3f);



         if(muzzelFlash != null)
        {
            GameObject currentMuzzelFlash = Instantiate(muzzelFlash,attackPoint.position,Quaternion.identity);
            Destroy(currentMuzzelFlash,5f);
        }




        // Add force to object
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(hit.normal * -impactForce);
        }



          if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShotting);
            allowInvoke = false;
        }

   }


    private void ResetShot()
    {
        allowInvoke = true;
    }



}
