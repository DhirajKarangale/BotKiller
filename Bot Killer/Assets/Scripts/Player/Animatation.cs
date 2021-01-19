using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatation : MonoBehaviour
{
    [SerializeField] Gun gun;
    public WeaponManager weaponManager;
    [SerializeField] Animator animator;
    private void Update()
    {
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        if((x==0) && (z==0) && (gun.reloading == false) && (weaponManager.shotting == false))
        {
           animator.SetBool("Breath",true);
           animator.SetBool("Original",false);
           animator.SetBool("Reload",false);
           animator.SetBool("Runing",false);
        }
        else if((x != 0) || (z != 0) || (gun.reloading == false) && (weaponManager.shotting == false))
        {
           animator.SetBool("Breath",false);
           animator.SetBool("Original",false);
           animator.SetBool("Reload",false);
           animator.SetBool("Runing",true);
        }
        
      if(gun.reloading)
       {
           animator.SetBool("Breath",false);
           animator.SetBool("Original",false);
           animator.SetBool("Reload",true);
           animator.SetBool("Runing",false);
        }

    }
}
