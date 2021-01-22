using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilRotation : MonoBehaviour
{
    public WeaponButton weaponButton;
    [SerializeField] Vector3 recoil;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localEulerAngles;
    }

    private void Update()
    {
        if(weaponButton.shotting == true)
        {
            transform.localEulerAngles += recoil;
        }

        if(weaponButton.shotting == false)
        {
            transform.localEulerAngles = originalPosition;
        }
    }

}
