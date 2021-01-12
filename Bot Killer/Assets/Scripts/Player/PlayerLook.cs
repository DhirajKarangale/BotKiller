using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float lookSensitivity = 400f;
    [SerializeField] Transform playerBody;
    private float xRotation = 0f;

    private void Update()
    {
        look();
    }

    private void look()
    {
        lookLeftRight();
        lookUpDown();
    }

    // Look Left ,Right by mouse.
    private void lookLeftRight() 
    {
        float lookX = SimpleInput.GetAxis("MouseX") * lookSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * lookX);
    }

    // Look Up ,Down by mouse.
    private void lookUpDown() 
    {
        float lookY = SimpleInput.GetAxis("MouseY") * lookSensitivity * Time.deltaTime;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
