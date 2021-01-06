using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float lookSensitivity = 400f;
    [SerializeField] Transform playerBody;
    private float xRotation = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

    private void lookLeftRight()
    {
        float lookX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * lookX);
    }
 
    private void lookUpDown()
    {
        float lookY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
