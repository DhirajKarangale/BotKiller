using UnityEngine;

public class WeaponButton : MonoBehaviour
{ 
    public bool shotting,isReload;
    
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
  
}
