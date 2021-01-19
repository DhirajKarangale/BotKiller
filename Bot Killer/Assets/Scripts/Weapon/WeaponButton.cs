using UnityEngine;

public class WeaponButton : MonoBehaviour
{ 
    [SerializeField] WeaponManager weaponManager;
    
    public void PointerUp()
    {
      weaponManager.shotting = false;
    }

    public void PointerDown()
    {
      weaponManager.shotting = true;
    }
    
    public void Reload()
    {
        weaponManager.isReload = true;
    }
  
}
