using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Player Attributes")]
    [SerializeField] byte walkSpeed = 25;
    [SerializeField] float gravity = -25f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] byte thrustSpeed = 2;

    [Header("Points")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem thrustEffect;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] LayerMask groundMask;
    
    [Header("Health")]
    [SerializeField] float health = 500;
    [SerializeField] float regainHealth;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject deathEffect;
    [SerializeField] float healthIncreaseAfterTime;
    private GameObject currentDeathEffect;
    private float currentHealth;
    private bool isRegainHealth,isPlayerHit;
    public bool isPlayerAlive;

    [Header("Thrust")]
    [SerializeField] float thrustFuel;
    private float currentThrustFuel;
    [SerializeField] Slider thrustSlider;
    [SerializeField] float decreaseThrustFuel,increaseThrustFuel;

    [Header("Prefrences")]
    [SerializeField] GameObject fps;
    [SerializeField] GameObject UIScreen;
    [SerializeField] GameObject GunsContainer;

    private Vector3 gravityDownVelocity;
    private bool isGrounded,isThrust;
    private Rigidbody rigidBody;
    [SerializeField] GameObject hitScreen;


    private void Start()
    {
        isPlayerAlive = true;
        currentHealth = health;
        currentThrustFuel = thrustFuel;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        healthSlider.value = CalculateHealth();
        thrustSlider.value = currentThrustFuel/thrustFuel;
    }   
    
    private void Update()
    {
        Movement();
        Gravity();  
        HitScreen(); 
     
        if(isThrust && currentThrustFuel>0)
        {
            Thrust();
        }
       else
       {
           thrustEffect.Stop();
           audioSource.Stop();
       }
       healthSlider.value = CalculateHealth();
       thrustSlider.value = currentThrustFuel/thrustFuel;
      if(currentHealth == health)
      {
        isRegainHealth = false;
      }

       currentHealth = Mathf.Clamp(currentHealth,0f,health);
       if(currentHealth <= health/2)
       {
         isRegainHealth = true;  
       }
       if(isRegainHealth && !isPlayerHit) 
       {
           Invoke("RegainHealth",healthIncreaseAfterTime);
       }
        
       currentThrustFuel = Mathf.Clamp(currentThrustFuel,0f,thrustFuel);
       if(isThrust)
       {
           currentThrustFuel -= decreaseThrustFuel * Time.deltaTime;
       }
       if(!isThrust && currentThrustFuel<thrustFuel)
       {
           currentThrustFuel += increaseThrustFuel * Time.deltaTime;
       }
       
    }

    public void PointerUp()
    {
        isThrust = false;
    }

    public void PointerDown()
    {
        isThrust = true;
    }

    private void Movement()  // Adding left, right, forward, backword movement; 
    {
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);
        controller.Move(move * walkSpeed * Time.deltaTime);
      
      animator.SetFloat("VelocityX",x);
      animator.SetFloat("VelocityZ",z);


    }

    private void Gravity()   // Adding Gravity.
    {
        gravityDownVelocity.y += gravity * Time.deltaTime;
        CheckGround();   // To stop decreasing velocity when on ground.
        controller.Move(gravityDownVelocity * Time.deltaTime);
    }

    private void CheckGround()  // Check the player is on ground or not.
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if((isGrounded) && (gravityDownVelocity.y<0))
        {
            gravityDownVelocity.y = -2f;
        }
    }
   
    private void Thrust()
    {
        gravityDownVelocity.y = Mathf.Sqrt(thrustSpeed * (-2f) * gravity);
        thrustEffect.Play();
        audioSource.PlayOneShot(thrustSound);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "EnemyBullet")
        { 
            isPlayerHit = true;
            PlayerHit();
        }
        else
        {
            isPlayerHit = false;
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
        if(currentHealth<=0)
        {
        Invoke("DestroyPlayer",0.3f);         
        }
    }

    private void DestroyPlayer()
    {
      isPlayerAlive = false;
      UIScreen.SetActive(false);
      GunsContainer.SetActive(false);
      currentDeathEffect = Instantiate(deathEffect,transform.position,Quaternion.identity);
      Destroy(currentDeathEffect,3f);
      Destroy(fps);
    }

     float CalculateHealth()
    {
      return currentHealth/health;
    }

    private void RegainHealth()
    {
        currentHealth += regainHealth * Time.deltaTime;
        isRegainHealth = false;
    }

}
