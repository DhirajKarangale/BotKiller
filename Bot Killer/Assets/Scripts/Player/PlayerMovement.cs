using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
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
    [SerializeField] LayerMask groundMask;

    [Header("Thrust")]
    [SerializeField] ParticleSystem thrustEffect;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] Slider thrustSlider;
    [SerializeField] float thrustFuel;
    private float currentThrustFuel;
    [SerializeField] float decreaseThrustFuel,increaseThrustFuel,increaseThrustFuelAfterTime;

    private Vector3 gravityDownVelocity;
    private bool isGrounded,isThrust;
    private Rigidbody rigidBody;
    

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentThrustFuel = thrustFuel;
        thrustSlider.value = currentThrustFuel/thrustFuel;
    }   
    
    private void Update()
    {
        Movement();
        Gravity();  
        
     
        if(isThrust && currentThrustFuel>0)
        {
            Thrust();
             audioSource.clip = thrustSound;
             audioSource.Play();
        }
       else
       {
           thrustEffect.Stop();
           audioSource.clip = null;
       }
       ThrustSlideBar();
      
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
        currentThrustFuel -= decreaseThrustFuel * Time.deltaTime;
        gravityDownVelocity.y = Mathf.Sqrt(thrustSpeed * (-2f) * gravity);
        thrustEffect.Play();
       
    }

    private void ThrustSlideBar()
    {
       thrustSlider.value = currentThrustFuel/thrustFuel;
       currentThrustFuel = Mathf.Clamp(currentThrustFuel,0f,thrustFuel);
       if(!isThrust && currentThrustFuel<thrustFuel) Invoke("IncreaseThrustFuel",increaseThrustFuelAfterTime);
    }

    private void IncreaseThrustFuel()
    {
        currentThrustFuel += increaseThrustFuel * Time.deltaTime;
    }
}
