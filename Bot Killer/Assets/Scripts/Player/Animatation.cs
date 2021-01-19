using UnityEngine;

public class Animatation : MonoBehaviour
{
    public Gun gun;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] Animator animator;
    private void Update()
    {
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        if((x==0) && (z==0) && (gun.reloading == false) && (weaponButton.shotting == false))
        {
           animator.SetBool("Breath",true);
           animator.SetBool("Original",false);
           animator.SetBool("Reload",false);
          // animator.SetBool("Runing",false);
        }
        else if((x != 0) || (z != 0) && (gun.reloading == false) && (weaponButton.shotting == false))
        {
           animator.SetBool("Breath",false);
           animator.SetBool("Original",true);
           animator.SetBool("Reload",false);
         //  animator.SetBool("Runing",true);
        }
        
       else if(gun.reloading)
       {
           animator.SetBool("Breath",false);
           animator.SetBool("Original",false);
           animator.SetBool("Reload",true);
         //  animator.SetBool("Runing",false);
        }
          
    }
}
