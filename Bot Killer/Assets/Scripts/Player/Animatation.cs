using UnityEngine;

public class Animatation : MonoBehaviour
{
  
    public bool reloading;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] Animator animator;
    private void Update()
    {
             
      
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        if(weaponButton.shotting && ((x != 0) || (z != 0)))
       {
         animator.SetBool("OriginalPos",true);
         animator.SetBool("Breath",false);
         animator.SetBool("Reload",false);
         animator.SetBool("Runing",false);
       }
     

        if((x==0) && (z==0) && (reloading == false) && (weaponButton.shotting == false))
        {
           animator.SetBool("Breath",true);
           animator.SetBool("Reload",false);
           animator.SetBool("Runing",false);
           animator.SetBool("OriginalPos",false);
        }

        if((weaponButton.shotting == false) && (reloading == false) && ((x != 0) || (z != 0)))
        {
           animator.SetBool("Breath",false);
           animator.SetBool("Reload",false);
           animator.SetBool("Runing",true);
           animator.SetBool("OriginalPos",false);
        }
        
       if(reloading == true)
       {
           animator.SetBool("Breath",false);
           animator.SetBool("Reload",true);
           animator.SetBool("Runing",false);
           animator.SetBool("OriginalPos",false);
        }
    }
}
