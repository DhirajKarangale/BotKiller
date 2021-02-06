using UnityEngine;
using UnityEngine.UI;

public class Health_Dye : MonoBehaviour
{
   [Header("Health")]
   [SerializeField] Slider healthSlider;
   [SerializeField] float health;
   [SerializeField] float regainHealth;
   [SerializeField] float healthIncreaseAfterTime;
   private float currentHealth;
   private bool isPlayerHit,allowRegainHealth;
   public bool isPlayerAlive,isHealthPackTrigger;
    

    [Header("Death")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject hitScreen;
    private GameObject currentDeathEffect;

    [Header("Refrences")]
    [SerializeField] GameObject fps;

    private void Start()
    {
        isPlayerAlive = true;
        currentHealth = health;
        healthSlider.value = currentHealth/health;
    }

    private void Update()
    {
        healthSlider.value = currentHealth/health;

        if(currentHealth<health && isHealthPackTrigger)  currentHealth = health; 
        HitScreen(); 
        RegainHealthController();
    }

    private void RegainHealthController()
    {
       currentHealth = Mathf.Clamp(currentHealth,0f,health);
       if(currentHealth == health/2) 
       {
           allowRegainHealth = false;
       }
       if(currentHealth <= health/2 && !isPlayerHit) allowRegainHealth = true;
       else allowRegainHealth = false;
       
       if(allowRegainHealth) Invoke("RegainHealth",healthIncreaseAfterTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "EnemyBullet")
        { 
            isHealthPackTrigger = false;
            PlayerHit();
            isPlayerHit = true;
        }
        if(collider.gameObject.tag == "HealthPack")
        {
            isPlayerHit = false;
            isHealthPackTrigger = true;
        }
        else
        {
            isPlayerHit = false;
            isHealthPackTrigger = false;
        } 
    }

    private void PlayerHit()
    {
        var color = hitScreen.GetComponent<Image>().color;
        color.a = 0.6f;
        hitScreen.GetComponent<Image>().color = color;
    }

    private void HitScreen()
    {
         if(hitScreen != null)
         {
             if(hitScreen.GetComponent<Image>().color.a > 0)
             {
                 var color = hitScreen.GetComponent<Image>().color;
                 color.a -= 0.05f;
                 hitScreen.GetComponent<Image>().color = color;
             }
         }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth<=0) Invoke("DestroyPlayer",0.3f);
    }

    private void DestroyPlayer()
    {
      isPlayerAlive = false;
      currentDeathEffect = Instantiate(deathEffect,transform.position,Quaternion.identity);
      Destroy(currentDeathEffect,3f);
      Destroy(fps,0.3f);
    }
    
    private void RegainHealth()
    {
      if(allowRegainHealth && !isPlayerHit)
      {
        allowRegainHealth = false;
        currentHealth += regainHealth * Time.deltaTime;
      }
    }
}
