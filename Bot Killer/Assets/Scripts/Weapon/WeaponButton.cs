using UnityEngine;


public class WeaponButton : MonoBehaviour
{ 
    public bool shotting,isReload,isPickUp,isDrop,isScope,throwGranide,isThrust;

     public void ThrustPointerUp()
    {
        isThrust = false;
    }

    public void ThrustPointerDown()
    {
        isThrust = true;
    }

    public void AttackButtonPointerUp()
    {
      shotting = false;
    }

    public void AttackButtonPointerDown()
    {
      shotting = true;
    }
        
    public void Reload()
    {
      isReload = true;
    }

     public void PickUpDropButtonPointerUP()
    {
      isPickUp = false;
    }

    public void PickUpDropButtonPointerDown()
    {
      isPickUp = true;
    }
    
   public void ScopeButton()
   {
     isScope = !isScope;
   }

     public void ThrowGranide()
     {
      throwGranide = true;
     }
}
