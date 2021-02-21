using UnityEngine;
using UnityEngine.UI;

public class Health_Dye_Trigger : MonoBehaviour
{
   [Header("Health")]
   [SerializeField] Slider healthSlider;
   public float health;
   [SerializeField] float regainHealth;
   [SerializeField] float healthIncreaseAfterTime;
   public float currentHealth;
   private bool isPlayerHit,allowRegainHealth;
   public bool isPlayerAlive,isHealthPackTrigger,isGranedeBoxTrigger;

    [Header("Death")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject hitScreen;
    [SerializeField] AudioClip hurtSound;
    private GameObject currentDeathEffect;

    [Header("Refrences")]
    [SerializeField] GameObject fps;
    [SerializeField] GameObject firstCamRef;
    [SerializeField] GameObject cam2;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlayerAlive = true;
        currentHealth = health;
        healthSlider.value = currentHealth/health;
    }

    private void Update()
    {
        healthSlider.value = currentHealth/health;

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
            audioSource.PlayOneShot(hurtSound,0.8f);
            isPlayerHit = true;
            isHealthPackTrigger = false;
            isGranedeBoxTrigger =false;
            PlayerHit();
        }
        if(collider.gameObject.tag == "HealthPack")
        {
            isPlayerHit = false;
            isHealthPackTrigger = true;
            isGranedeBoxTrigger = false;
        }
        if(collider.gameObject.tag == "GranedeBox")
        {
            isPlayerHit = false;
            isHealthPackTrigger = false;
            isGranedeBoxTrigger = true;
        }
    }

    private void PlayerHit()
    {
        var color = hitScreen.GetComponent<Image>().color;
        color.a = 0.9f;
        hitScreen.GetComponent<Image>().color = color;
    }

    private void HitScreen()
    {
         if(hitScreen != null)
         {
             if(hitScreen.GetComponent<Image>().color.a > 0)
             {
                 var color = hitScreen.GetComponent<Image>().color;
                 color.a -= 0.03f;
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
      Destroy(firstCamRef);
      cam2.SetActive(true);
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
