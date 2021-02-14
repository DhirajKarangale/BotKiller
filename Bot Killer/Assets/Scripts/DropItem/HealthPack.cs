using UnityEngine;

public class HealthPack : MonoBehaviour
{
   private Health_Dye_Trigger playerDye;
   [SerializeField] GameObject destroyEffect;

   private void Start()
   {
      playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
   }

   private void Update()
   {
       if(playerDye.isHealthPackTrigger && (playerDye.currentHealth<playerDye.health))
       {
         playerDye.currentHealth = playerDye.health;
         GameObject currentDestroyEffect = Instantiate(destroyEffect,transform.position,Quaternion.identity);
         Destroy(currentDestroyEffect,2f); 
         Destroy(gameObject,0.3f);
       }
   }
}
