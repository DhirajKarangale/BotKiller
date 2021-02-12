using UnityEngine;

public class HealthPack : MonoBehaviour
{
   private Health_Dye playerDye;
   [SerializeField] GameObject destroyEffect;

   private void Start()
   {
      playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye>();
   }

   private void Update()
   {
       if(playerDye.isHealthPackTrigger && (playerDye.currentHealth<playerDye.health))
       {
         playerDye.currentHealth = playerDye.health;
         Instantiate(destroyEffect,transform.position,Quaternion.identity);
         Destroy(gameObject,0.3f);
       }
   }
}
