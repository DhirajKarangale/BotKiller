using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] float speed = 25f;
    [SerializeField] float gravity = -25f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float jumpHeight = 5f;

    [Header("Points")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    private Vector3 gravityDownVelocity;
    private bool isGrounded;

    private void Update()
    {
        Movement();
        Gravity();   
        Jump();
    }

    private void Movement()  // Adding left, right, forward, backword movement; 
    {
        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);
        controller.Move(move * speed * Time.deltaTime);
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

    public void JumpButton()
    {
       if(isGrounded)
        {
            gravityDownVelocity.y = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
        }
    }
}
