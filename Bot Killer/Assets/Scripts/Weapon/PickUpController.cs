using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Gun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;
    public WeaponButton weaponButton;
    [SerializeField] GameObject ammoTextBG;

    public byte pickUpRange;
    public byte dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    { 
        //Setup
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
          
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
            
        }
    }

    private void Update()
    {
      if(slotFull == true)
      {
        gunScript.ammoDisplay.enabled = true;
        ammoTextBG.SetActive(true);
      }
      if(slotFull == false)
      {
        gunScript.ammoDisplay.enabled = false;
         ammoTextBG.SetActive(false);
      }
     
        //Check if player is in range 
        Vector3 distanceToPlayer = player.position - transform.position;
        if(distanceToPlayer.magnitude <= pickUpRange && !slotFull && !equipped && weaponButton.isPickUp) PickUp();
        //Drop if equipped 
        if ( slotFull && equipped && weaponButton.isPickUp) Drop();

        
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        gunScript.enabled = true;
        weaponButton.isPickUp = false;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        gunScript.enabled = false;
        weaponButton.isPickUp = false;
    }
}
