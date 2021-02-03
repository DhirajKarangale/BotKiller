using UnityEngine;

public class Attack_Follow : MonoBehaviour
{
  [SerializeField] byte speed;
  [SerializeField] byte followRange;
  [SerializeField] byte attackRange;
  [SerializeField] byte stopDistance;
  [SerializeField] byte retriveDistance;
  [SerializeField] float startTimeBetweenShot;
  [SerializeField] float flashTime;
  private float timeBetweenShot;
  private float distanseBetwwenEnemyAndPlayer;

  private Transform player;
  public Transform attackPoint;
  [SerializeField] Petroling petroling;
  [SerializeField] GameObject bullets;
  public MeshRenderer meshRenderer;
  public bool hitEnemy;
  private GameObject currentBullet;
  private Color originalColor;

  private void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    timeBetweenShot = startTimeBetweenShot;
    originalColor = meshRenderer.material.color;
  }   

  private void Update()
  {  
    // Enemy Hit Effect.
    if(hitEnemy)
    {
     meshRenderer.material.color = Color.red;
     Invoke("ResetColor",flashTime);
    }
    // Enemy look at player.
    if(petroling.isPetroling == false) 
    {
         Vector3 lookVector = player.transform.position - transform.position;
         lookVector.y = transform.position.y;
         Quaternion rot = Quaternion.LookRotation(-lookVector);
         transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }

    distanseBetwwenEnemyAndPlayer = Vector3.Distance(transform.position,player.position); // Distanse between Enemy and player.

    if((distanseBetwwenEnemyAndPlayer<followRange) && (distanseBetwwenEnemyAndPlayer>stopDistance))
    {
       transform.position = Vector3.MoveTowards(transform.position,player.position,(speed * Time.deltaTime));  // Follow to player.
       petroling.isPetroling = false;
    }
    else if((distanseBetwwenEnemyAndPlayer<followRange) && (distanseBetwwenEnemyAndPlayer<=stopDistance) && (distanseBetwwenEnemyAndPlayer>=retriveDistance))
    {
        transform.position = this.transform.position; // Stop Enemy.
        petroling.isPetroling = false;
    }
    else if((distanseBetwwenEnemyAndPlayer<followRange) && (distanseBetwwenEnemyAndPlayer<followRange))
    {
        transform.position = Vector3.MoveTowards(transform.position,player.position,(-speed * Time.deltaTime)); // Reverse Back Enemy.
        petroling.isPetroling = false;
    }
    else if(distanseBetwwenEnemyAndPlayer>followRange)
    {
        petroling.isPetroling = true; 
    }
    if(distanseBetwwenEnemyAndPlayer<=attackRange)
     { 
        petroling.isPetroling = false;
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
 
  private void ResetColor()
  {
    meshRenderer.material.color = originalColor;
  }
}
