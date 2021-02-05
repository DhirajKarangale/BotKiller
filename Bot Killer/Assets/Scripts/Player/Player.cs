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
    [SerializeField] float regainHealth = 50;
    [SerializeField] Slider slider;
    [SerializeField] GameObject deathEffect;
    private GameObject currentDeathEffect;
    public float currentHealth;
    private bool isRegainHealth;
    public bool isPlayerAlive;

    [Header("Prefrences")]
    [SerializeField] GameObject fps;
    [SerializeField] GameObject UIScreen;
    [SerializeField] GameObject GunsContainer;
    [SerializeField] GameObject cam2;
    

    private Vector3 gravityDownVelocity;
    private bool isGrounded,isThrust;
    private Rigidbody rigidBody;
    [SerializeField] GameObject hitScreen;


    private void Start()
    {
        isPlayerAlive = true;
        currentHealth = health;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        slider.value = CalculateHealth();
    }   
    
    private void Update()
    {
        if(isPlayerAlive)
        {
            cam2.SetActive(false);
        }
        else
        {
            cam2.SetActive(true);
        }
        Movement();
        Gravity();  
        HitScreen(); 
     
        if(isThrust)
        {
            Thrust();
        }
       else
       {
           thrustEffect.Stop();
           audioSource.Stop();
       }
       slider.value = CalculateHealth();

       if(currentHealth<health)
       {
         isRegainHealth = true;  
       }
       if(isRegainHealth) 
       {
           Invoke("RegainHealth",5f);
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
            PlayerHit();
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
        currentHealth += regainHealth;
        isRegainHealth = false;
    }

}
