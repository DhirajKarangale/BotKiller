using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    // References
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;

    // Player settings
    [SerializeField] private float moveInputDeadZone;

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;
    public static float moveSpeed = 21f;
    private float currentMoveSpeed = moveSpeed;
    public Vector2 movementDirection;


    [Header("Player Attributes")]
    [SerializeField] float gravity = -25f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] byte thrustSpeed = 2;

    [Header("Points")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] WeaponButton weaponButton;

    [Header("Thrust")]
    [SerializeField] ParticleSystem thrustEffect;
    [SerializeField] Slider thrustSlider;
    [SerializeField] float thrustFuel;
    private float currentThrustFuel;
    [SerializeField] float decreaseThrustFuel, increaseThrustFuel, increaseThrustFuelAfterTime;

    [Header("Animation")]
    [SerializeField] Animator legsAnimation;
    [SerializeField] float acceleration = 2f;
    private Vector3 currPlayerPos;


    private Vector3 gravityDownVelocity;
    private bool isGrounded, isThrust;
    public bool isMoving, isRotating;





    // Start is called before the first frame update
    private void Start()
    {
        currPlayerPos = transform.position;

        currPlayerPos = movementDirection;
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);


        currentThrustFuel = thrustFuel;
        thrustSlider.value = currentThrustFuel / thrustFuel;

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentMoveSpeed = moveSpeed;

        // Handles input
        GetTouchInput();


        if (rightFingerId != -1)
        {
            // Ony look around if the right finger is being tracked
            //  Debug.Log("Rotating");
            isRotating = true;
            isMoving = false;
            LookAround();
        }

        if (leftFingerId != -1)
        {
            // Ony move if the left finger is being tracked
            //  Debug.Log("Moving");
            isRotating = false;
            isMoving = true;
            Move();
        }

        // Giving Gravity
        Gravity();


        // Giving Thrust
        if (weaponButton.isThrust && currentThrustFuel > 0)
        {
            Thrust();
        }
        else
        {
            thrustEffect.Stop();
        }
        ThrustSlideBar();


        // Decrease Speed Whwn scope is On.
        if (Gun.isScopeOn)
        {
            currentMoveSpeed = currentMoveSpeed / 3;
        }
        else
        {
            currentMoveSpeed = currentMoveSpeed;
        }

        // Legs Animation.
        if (currPlayerPos == transform.position)
        {
            legsAnimation.SetFloat("VelocityX", 0);
            legsAnimation.SetFloat("VelocityZ", 0);
        }
        else
        {
            legsAnimation.SetFloat("VelocityX", movementDirection.x * acceleration);
            legsAnimation.SetFloat("VelocityZ", movementDirection.y * acceleration);
        }
        currPlayerPos = transform.position;
    }

    void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        //   Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        // Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * OptionMenu.lookSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId)
                    {

                        // calculating the position delta from the start position
                        moveInput = t.position - moveTouchStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {

        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {

        // Don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        // Multiply the normalized direction by the speed
        movementDirection = moveInput.normalized * currentMoveSpeed * Time.deltaTime;
        // Move relatively to the local transform's direction
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);



    }

    private void Gravity()   // Adding Gravity.
    {
        gravityDownVelocity.y += gravity * Time.deltaTime;
        CheckGround();   // To stop decreasing velocity when on ground.
        characterController.Move(gravityDownVelocity * Time.deltaTime);
    }

    private void CheckGround()  // Check the player is on ground or not.
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if ((isGrounded) && (gravityDownVelocity.y < 0))
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
        thrustSlider.value = currentThrustFuel / thrustFuel;
        currentThrustFuel = Mathf.Clamp(currentThrustFuel, 0f, thrustFuel);
        if (!isThrust && currentThrustFuel < thrustFuel) Invoke("IncreaseThrustFuel", increaseThrustFuelAfterTime);
    }

    private void IncreaseThrustFuel()
    {
        currentThrustFuel += increaseThrustFuel * Time.deltaTime;
    }
}