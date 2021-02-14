using UnityEngine;

public class Animatation : MonoBehaviour
{
  
    public bool reloading;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] Animator animator;
    private PlayerMovement player;
    private Health_Dye_Trigger playerDye;

   private void Start()
   {
      playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
   }

    private void Update()
    {
      if(playerDye.isPlayerAlive)
      {
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

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
