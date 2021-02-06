using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{

  [Header("Follow")]
  [SerializeField] byte followSpeed;
  [SerializeField] byte followRange;
  [SerializeField] byte attackRange;
  [SerializeField] byte stopDistance;
  [SerializeField] byte retriveDistance;
  private Player player;
  private float distanseBetwwenEnemyAndPlayer;

  [Header("Attack")]
  [SerializeField] GameObject bullets;
  [SerializeField] Transform attackPoint;
  [SerializeField] float startTimeBetweenShot;
  private GameObject currentBullet;
  private float timeBetweenShot;

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
  public float waitTime;
  private bool isPetroling;

  [Header("Flash")]
  [SerializeField] MeshRenderer meshRenderer;

  [Header("Death Effect")]
  [SerializeField] GameObject deathEffect;
  private GameObject currentDeathEffect;
  private CamShake camShake;

  [Header("Health")]
  [SerializeField] float health;
  private float currentHealth;
  [SerializeField] GameObject healthBarUI;
  [SerializeField] Slider slider;
  private GameObject cam2;

  private void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    cam2 = GameObject.FindGameObjectWithTag("Cam2");
    
    
    timeBetweenShot = startTimeBetweenShot;

    // Petrolling
     waitTime = startWaitTime;
     // Setting move point first time.
     movePoint.position = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),Random.Range(minZ,maxZ));
     isPetroling =true;
       
     // Declearing and finding camera shake script 
     camShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CamShake>();

     currentHealth = health;
     slider.value = CalculateHealth();
  }   

  private void Update()
  { 
    if(!player.isPlayerAlive)
     {
       Invoke("Petrolling",0.5f);
       cam2.SetActive(true);
       player.enabled = false;
     }
     else
     {
       cam2.SetActive(false);
      slider.value = CalculateHealth();
     if(currentHealth<health)
     {
       healthBarUI.SetActive(true);
     }
     if(currentHealth == health)
     {
       healthBarUI.SetActive(false);
     }

     // Distanse between Enemy and player.
    distanseBetwwenEnemyAndPlayer = Vector3.Distance(transform.position,player.transform.position);
     
     if(isPetroling) Petrolling();
     else Look(); 

     Follow();
     Attack();
     }
  }

   private void Follow()
   {
       if((distanseBetwwenEnemyAndPlayer<followRange) && (distanseBetwwenEnemyAndPlayer>stopDistance))
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
      if(distanseBetwwenEnemyAndPlayer<=attackRange)
     { 
        isPetroling = false;
        if(timeBetweenShot<=0)
        {
         currentBullet = Instantiate(bullets,attackPoint.position,Quaternion.identity); // Shoot
         timeBetweenShot = startTimeBetweenShot;
        }
        else
        {
           timeBetweenShot -= Time.deltaTime;
        }

        Destroy(currentBullet,3f);
     }
   }

   private void Petrolling()
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

   // Enemy look at player.
  private void Look()
  {
   Vector3 lookVector = player.transform.position - transform.position;
   lookVector.y = transform.position.y;
   Quaternion rot = Quaternion.LookRotation(-lookVector);
   transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
  }


   public void OnTriggerEnter(Collider collider)
   {
     if(collider.gameObject.tag == "Bullet")
     {
       StartCoroutine(FlashRed());
     }
   }
   public IEnumerator FlashRed()
   {
     meshRenderer.material.color = Color.red;
     yield return new WaitForSeconds(0.1f);
     meshRenderer.material.color = Color.white;
   }     

  public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if(currentHealth<=0)
        {
         DestroyEnemy();         
        }
    }

    private void DestroyEnemy()
    {
      camShake.Shake();
      currentDeathEffect = Instantiate(deathEffect,transform.position,Quaternion.identity);
      Destroy(currentDeathEffect,3f);
      Destroy(gameObject,0.3f);
    }

    float CalculateHealth()
    {
      return currentHealth/health;
    }
}
