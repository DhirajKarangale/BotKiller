using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] float walkSpeed = 25f;
    [SerializeField] float gravity = -25f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float thrustSpeed = 3000f;

    [Header("Points")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Animator animator;

    public bool isThrust;
    private Vector3 gravityDownVelocity;
    private bool isGrounded;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }   
    
    private void Update()
    {
        Movement();
        Gravity();   
        Jump();
        Animation();
        if(Input.GetKeyDown(KeyCode.P))
        {
            Thrust();
        }
    }

    public void PointerUp()
    {
      //  isThrust = false;
    }

    public void PointerDown()
    {
       // isThrust = true;
    }

    private void Movement()  // Adding left, right, forward, backword movement; 
    {
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);
        controller.Move(move * walkSpeed * Time.deltaTime);
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

    private void Jump() // Adding Jump to Player.
    {
        if (Input.GetButtonDown("Jump"))
        {
            JumpButton(); 
        }
    }

    private void JumpButton()
    {
       if(isGrounded)
        {
            gravityDownVelocity.y = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
        }
    }

    private void Animation()
    {
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        if(x > 0)
        {
            animator.SetBool("Left",true);
            animator.SetBool("Right",false);
            animator.SetBool("Forward",false);
            animator.SetBool("Backward",false);
            animator.SetBool("OriginalPos",false);
        }
        if(x < 0)
        {
            animator.SetBool("Left",false);
            animator.SetBool("Right",true);
            animator.SetBool("Forward",false);
            animator.SetBool("Backward",false);
            animator.SetBool("OriginalPos",false);
        }

        if(z < 0)
        {
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            animator.SetBool("Forward",false);
            animator.SetBool("Backward",true);
            animator.SetBool("OriginalPos",false);
        }

        if(z > 0)
        {
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            animator.SetBool("Forward",true);
            animator.SetBool("Backward",false);
            animator.SetBool("OriginalPos",false);
        }
       if((x==0) && (z==0))
        {
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            animator.SetBool("Forward",false);
            animator.SetBool("Backward",false);
            animator.SetBool("OriginalPos",true);
        }
       
    }
     private void Thrust()
        {
            Debug.Log("Thrust Sucess");
            //rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            rigidBody.AddForce(0, thrustSpeed, 0, ForceMode.Impulse);
        }

}
