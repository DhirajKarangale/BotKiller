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
       if(playerDye.isHealthPackTrigger)
       {
         Instantiate(destroyEffect,playerDye.transform.position,playerDye.transform.rotation);
         Destroy(gameObject,0.3f);
       }
   }
}
