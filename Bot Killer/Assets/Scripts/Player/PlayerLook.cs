using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] float sensitivity = 170f;
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
        float lookX = SimpleInput.GetAxis("MouseX") * sensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * lookX);
    }

    // Look Up ,Down by mouse.
    private void lookUpDown() 
    {
        float lookY = SimpleInput.GetAxis("MouseY") * sensitivity * Time.deltaTime;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
