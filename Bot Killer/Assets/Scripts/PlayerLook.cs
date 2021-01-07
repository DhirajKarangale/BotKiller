using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float lookSensitivity = 400f;

    [SerializeField] Transform playerBody;

    private float xRotation = 0f;


    private void Start()
    {
        //  Cursor.lockState = CursorLockMode.Locked;  // To hide and lock cursor on game screen .
    }

    private void Update()
    {
        look();
    }

    private void look()
    {
        lookLeftRight();
        lookUpDown();
    }

    private void lookLeftRight() // Look Left ,Right by mouse.
    {
        float lookX = SimpleInput.GetAxis("MouseX") * lookSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * lookX);
    }
 
    private void lookUpDown() // Look Up ,Down by mouse.
    {
        float lookY = SimpleInput.GetAxis("MouseY") * lookSensitivity * Time.deltaTime;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
