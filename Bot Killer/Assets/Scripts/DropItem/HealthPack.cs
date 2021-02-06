using UnityEngine;

public class HealthPack : MonoBehaviour
{
   private Health_Dye playerDye;
   [SerializeField] GameObject destroyEffect;
   private GameObject currentDestroyEffect;

   private void Start()
   {
       playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye>();
   }

   private void Update()
   {
       if(playerDye.isHealthPackTrigger)
       {
         currentDestroyEffect = Instantiate(destroyEffect,transform.position,Quaternion.identity);
         Destroy(currentDestroyEffect);
         Destroy(gameObject,0.3f);
       }
   }
}
