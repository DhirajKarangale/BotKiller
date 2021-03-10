using UnityEngine;

public class Animatation : MonoBehaviour
{
  
    public bool reloading;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] Animator animator;
    private Player player;
    private Health_Dye_Trigger playerDye;

   private void Start()
   {
      playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
   }

    private void Update()
    {
      if(playerDye.isPlayerAlive)
      {
        float x = player.movementDirection.x;
        float z = player.movementDirection.y;

        if(weaponButton.shotting && ((x != 0) || (z != 0)))
       {
         animator.SetBool("OriginalPos",true);
         animator.SetBool("Breath",false);
         animator.SetBool("Reload",false);
         animator.SetBool("Runing",false);
         animator.SetBool("Scoped",false);
       }
     

        if((x==0) && (z==0) && (reloading == false) && (weaponButton.shotting == false))
        {
           animator.SetBool("Breath",true);
           animator.SetBool("Reload",false);
           animator.SetBool("Runing",false);
           animator.SetBool("OriginalPos",false);
           animator.SetBool("Scoped",false);
        }

        if((weaponButton.shotting == false) && (reloading == false) && ((x != 0) || (z != 0)))
        {
           animator.SetBool("Breath",false);
           animator.SetBool("Reload",false);
           animator.SetBool("Runing",true);
           animator.SetBool("OriginalPos",false);
           animator.SetBool("Scoped",false);
        }
        
       if(reloading == true)
       {
           animator.SetBool("Breath",false);
           animator.SetBool("Reload",true);
           animator.SetBool("Runing",false);
           animator.SetBool("OriginalPos",false);
           animator.SetBool("Scoped",false);
        }
      }

      if(weaponButton.isScope)
      {
         animator.SetBool("Breath",false);
         animator.SetBool("Reload",false);
         animator.SetBool("Runing",false);
         animator.SetBool("OriginalPos",false);
         animator.SetBool("Scoped",true);
      }
        
    }
}
