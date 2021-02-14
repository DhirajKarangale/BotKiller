using TMPro;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
   [SerializeField] GameObject granedePrefab;
   [SerializeField] GameObject granedeButton;
   [SerializeField] WeaponButton weaponButton;
   [SerializeField] TextMeshProUGUI granedeDisplay;
   [SerializeField] float throwForce;
   [SerializeField] float throwTime;
   [SerializeField] int noOfGranede;
   public int currentGranede;
   private float currentThrowTime;

   private void Start()
   {
       currentGranede = noOfGranede;
       currentThrowTime = 0;
   }
   
   private void Update()
   {
       currentThrowTime -= Time.deltaTime;
       if(currentThrowTime>0)
       {
           weaponButton.throwGranide = false;
       }
       if(currentThrowTime<=0)
       {
           currentThrowTime = 0;
       }
       if(weaponButton.throwGranide && (currentThrowTime<=0) && (currentGranede>0))
       {
           currentGranede--;
           currentThrowTime =throwTime;
           weaponButton.throwGranide = false;
           GameObject granede = Instantiate(granedePrefab,transform.position,transform.rotation);
           Rigidbody rigidbody = granede.GetComponent<Rigidbody>();
           rigidbody.AddForce(transform.forward * throwForce ,ForceMode.Impulse);
       }
       if(currentGranede<=0)
       {
           Invoke("DesableGranide",0.3f);
       }
       else
       {
            granedeButton.SetActive(true);
       }

       granedeDisplay.SetText(currentGranede + "");
   }

   private void DesableGranide()
   {
       granedeButton.SetActive(false);
   }
}
