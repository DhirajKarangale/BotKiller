using UnityEngine;

public class GameManager : MonoBehaviour
{
   private Health_Dye_Trigger playerDye;
   [SerializeField] GameObject UIScreen;
   [SerializeField] GameObject GunContainer;
   [SerializeField] GameObject itemToIntroduce;
   private PlayerMovement player;

   private void Start()
   {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
       playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
   }

   private void Update()
   {
     if(!playerDye.isPlayerAlive)
     {
         UIScreen.SetActive(false);
         GunContainer.SetActive(false);
         itemToIntroduce.SetActive(false);
         player.enabled = false;
         playerDye.enabled = false;
     }
   }
}
