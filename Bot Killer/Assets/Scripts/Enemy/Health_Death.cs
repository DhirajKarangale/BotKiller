using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health_Death : MonoBehaviour
{
  [Header("Death Effect")]
  [SerializeField] GameObject deathEffect;
  private GameObject currentDeathEffect;
  private CamShake camShake;
  [SerializeField] GameObject itemToDrop;
  // Flash
  [SerializeField] MeshRenderer meshRenderer;

  [Header("Health")]
  [SerializeField] Slider slider;
  [SerializeField] GameObject healthBarUI;
  [SerializeField] float health;
  private float currentHealth;

  private void Start()
  {
    currentHealth = health;
    slider.value =currentHealth/health;
     // Declearing and finding camera shake script 
     camShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CamShake>();
  }

  private void Update()
  {
    HealthBarController();
  }

  private void HealthBarController()
  {
    slider.value =currentHealth/health;
     if(currentHealth<health)
     {
       healthBarUI.SetActive(true);
     }
     if(currentHealth == health)
     {
       healthBarUI.SetActive(false);
     }
  }

  public void OnTriggerEnter(Collider collider)
   {
     if(collider.gameObject.tag == "Bullet")
     {
       StartCoroutine(FlashRed());
     }
   }

   public IEnumerator FlashRed()
   {
     meshRenderer.material.color = Color.red;
     yield return new WaitForSeconds(0.1f);
     meshRenderer.material.color = Color.white;
   }     

   public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth<=0)
        {
         DestroyEnemy();         
        }
    }

    private void DestroyEnemy()
    {
      camShake.Shake();
      currentDeathEffect = Instantiate(deathEffect,transform.position,Quaternion.identity);
      Destroy(currentDeathEffect,3f);
      Instantiate(itemToDrop,transform.position,Quaternion.identity);
      Destroy(gameObject,0.3f);
    }

}

