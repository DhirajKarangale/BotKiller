using UnityEngine;

public class WeaponButton : MonoBehaviour
{ 
    public bool shotting,isReload,isPickUp,isDrop;
    
    
     
    public void PointerUp()
    {
      shotting = false;
    }

    public void PointerDown()
    {
      shotting = true;
    }
    
    public void Reload()
    {
      isReload = true;
    }

    public void PickupButton()
    {
      isPickUp = true;
    }
    
    public void DropButton()
    {
      isDrop = true;
    }

}
