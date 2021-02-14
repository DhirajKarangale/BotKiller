using UnityEngine;

public class Grenade : MonoBehaviour
{
  [SerializeField] float delay;
  [SerializeField] float effectArea;
  [SerializeField] float explosionForce;
  [SerializeField] int damage;
  [SerializeField] GameObject granideEffect;
  private float countDown;
  private bool isExplode;
 
  private void Start()
  {
    countDown = delay;
  }

  private void Update()
  {
    countDown -= Time.deltaTime;
     if(countDown<=0 && !isExplode)
     {
        Explode();
     }
    
  }
   private void Explode()
     {
        isExplode = true;
        Instantiate(granideEffect,transform.position,transform.rotation);

          Collider[] colliderToDestroy =  Physics.OverlapSphere(transform.position,effectArea); // Finding the object near granide to destroy them.
          foreach (Collider nearByObject in colliderToDestroy) // Go through all object
          {
            ItemsDestroy item = nearByObject.GetComponent<ItemsDestroy>(); // Finding item to destroy 
            if(item != null)
            {
               item.TakeDamage(damage); // Damage Item
            }

            Health_Death enemyDye = nearByObject.GetComponent<Health_Death>(); // Finding enemy to destroy.
            if(enemyDye != null)
            {
              enemyDye.TakeDamage(damage); // Damage Enemy.
            }

            Health_Dye_Trigger playerDye = nearByObject.GetComponent<Health_Dye_Trigger>();
            if(playerDye != null)
            {
              playerDye.TakeDamage(damage);
            }
          }

           Collider[] colliderToMove =  Physics.OverlapSphere(transform.position,effectArea); // Finding the object near granide to move them.
           foreach (Collider nearByObject in colliderToMove)
           {
            Rigidbody rb = nearByObject.GetComponent<Rigidbody>(); 
            if(rb != null)
            {
              rb.AddExplosionForce(explosionForce,transform.position,effectArea); // Adding force to object
            }
           }
        Destroy(gameObject); 
     }

    
}
