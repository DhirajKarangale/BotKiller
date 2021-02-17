using UnityEngine;

public class GranedeBox : MonoBehaviour
{
    private Health_Dye_Trigger playerDye;
    private GrenadeThrow grenadeThrow;

    private void Start()
    {
      playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
      grenadeThrow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrenadeThrow>();
    }

    private void Update()
    {
        if(playerDye.isGranedeBoxTrigger)
        {
        grenadeThrow.currentGranede += 5;
        Destroy(gameObject);
        }
    }  
}
