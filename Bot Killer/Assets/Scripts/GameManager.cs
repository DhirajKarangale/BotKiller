using UnityEngine;

public class GameManager : MonoBehaviour
{
   private Health_Dye playerDye;
   [SerializeField] GameObject UIScreen;
   [SerializeField] GameObject GunContainer;
   [SerializeField] GameObject cam2;
   private PlayerMovement player;

   private void Start()
   {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
       playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye>();
   }

   private void Update()
   {
     if(!playerDye.isPlayerAlive)
     {
         UIScreen.SetActive(false);
         GunContainer.SetActive(false);
         cam2.SetActive(true);
         player.enabled = false;
         playerDye.enabled = false;
     }
   }
}
