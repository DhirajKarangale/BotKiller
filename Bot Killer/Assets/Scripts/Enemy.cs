using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   [Header("Refrences")]
   [SerializeField] NavMeshAgent agent;
   [SerializeField] Transform player;
   [SerializeField] LayerMask whatIsGround,whatIsPlayer;

  [Header("Petroling")]
   [SerializeField] Vector3 walkPoint;
   private bool walkPointSet;
   [SerializeField] float walkPointRange;

   [Header("Range")]
   [SerializeField] float sightRange;
   [SerializeField] float attackRange;
   private bool playerInSightRange,playerInAttackRange;

   [Space(10)]
   // Health 
   [SerializeField] int health;
 

    [Header("Shotting")]
    [SerializeField] GameObject bullets;
    [SerializeField] GameObject muzzelFlash;
    [SerializeField] Transform attackPoint;
    [SerializeField] int shootForce;
    [SerializeField] int impactForce;
    [SerializeField] float timeBetweenAttack;
    [SerializeField] int damage;
    private GameObject currentBullet;
    private Vector3 directionOfBullet;
    private Vector3 targetPoint;
    private bool alreadyAttack;
    RaycastHit hit;
   

   

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

         if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(hit.normal * -impactForce);
        }
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
     targetPoint = player.transform.position; 
     directionOfBullet = targetPoint - attackPoint.position;
     currentBullet = Instantiate(bullets,attackPoint.position,Quaternion.identity);
     currentBullet.transform.forward = directionOfBullet.normalized;

     currentBullet.GetComponent<Rigidbody>().AddForce(directionOfBullet.normalized * shootForce,ForceMode.Impulse);
     currentBullet.GetComponent<Rigidbody>().AddForceAtPosition(directionOfBullet.normalized * impactForce,targetPoint);
      
     Destroy(currentBullet,3f);
   }

}
