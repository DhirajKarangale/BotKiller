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
  [SerializeField] LevelManager levelManager;
  public bool isEnemyHit;
 
    // Flash
  [SerializeField] MeshRenderer meshRenderer;
  [SerializeField] Material originalColor;


  [Header("Health")]
  [SerializeField] Slider slider;
  [SerializeField] GameObject healthBarUI;
  [SerializeField] float health;
  private float currentHealth;

  private void Start()
  {
    originalColor.color =   meshRenderer.material.color;
    currentHealth = health;
    slider.value =currentHealth/health;
     // Declearing and finding camera shake script 
     camShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CamShake>();
    }

  private void Update()
  {
    HealthBarController();
        if(isEnemyHit)
        {
            Invoke("SetEnemyHitToFalse", 20f);
        }
  }

   private void SetEnemyHitToFalse()
    {
        isEnemyHit = false;
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

   public void FlashRed()
   {
     meshRenderer.material.color = Color.red;
     Invoke("ReturnToOriginalColor",0.13f);
   } 
   private void ReturnToOriginalColor()
   {
     meshRenderer.material.color = originalColor.color;
   }    

   public void TakeDamage(int damage)
   {
        isEnemyHit = true;
        currentHealth -= damage;
        if(currentHealth<=0)
        {
         DestroyEnemy();         
        }
   }

    public void DestroyEnemy()
    {
      levelManager.enemyDestroyed++;
      camShake.Shake();
      currentDeathEffect = Instantiate(deathEffect,transform.position,Quaternion.identity);
      Destroy(currentDeathEffect,3f);
      Invoke("DropItem", 0.1f);
      Destroy(gameObject,0.3f);
    }

    private void DropItem()
    {
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }

}

