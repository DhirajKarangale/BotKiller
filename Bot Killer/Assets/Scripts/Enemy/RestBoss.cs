using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestBoss : MonoBehaviour
{
   [Header("Attack")]
  [SerializeField] GameObject bullets;
  [SerializeField] Transform attackPoint;
  [SerializeField] float startTimeBetweenShot;
  [SerializeField] int bulletForce;
  [SerializeField] float impactForce;
   [SerializeField] byte attackRange;
  private RaycastHit hit;
  private GameObject currentBullet;
  private float timeBetweenShot;


   private float distanseBetwwenEnemyAndPlayer;
   private PlayerMovement player;


   private void Start()
   {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
       timeBetweenShot = startTimeBetweenShot;
   }

  private void Update()
  {
      // Distanse between Enemy and player.
       distanseBetwwenEnemyAndPlayer = Vector3.Distance(transform.position,player.transform.position);
       Look();
       Attack();

  }

  private void Look()
   {
    Vector3 lookVector = player.transform.position - transform.position;
    lookVector.y = transform.position.y;
    Quaternion rot = Quaternion.LookRotation(lookVector);
    transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
   }

    private void Attack()
   {
      Vector3 target = player.transform.position - transform.position;
      if(distanseBetwwenEnemyAndPlayer<=attackRange)
     { 
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
