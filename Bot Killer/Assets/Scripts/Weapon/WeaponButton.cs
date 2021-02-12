using UnityEngine;


public class WeaponButton : MonoBehaviour
{ 
    public bool shotting,isReload,isPickUp,isDrop,isScope;

     
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
    
}
