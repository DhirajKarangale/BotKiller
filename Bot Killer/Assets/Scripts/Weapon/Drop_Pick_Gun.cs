using UnityEngine;
using UnityEngine.UI;


public class Drop_Pick_Gun : MonoBehaviour
{
    [SerializeField] Gun gunScript;
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider coll;
    [SerializeField] Transform player, gunContainer, fpsCam;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] Button PickUpButton;


    [SerializeField] float dropForwardForce, dropUpwardForce,gunPickUpRange;
    [SerializeField] bool equipped;
    public static bool slotFull;
    private Vector3 distanceToPlayer;

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
        // Check if player is in range.
        distanceToPlayer = player.position - transform.position;
        PickUpButtonControl();  

        // Activate PickUp Button.
        if(!equipped && distanceToPlayer.magnitude<=gunPickUpRange && weaponButton.isPickUp && !slotFull) PickUp();

        // Activate Drop Button.
        if(equipped && weaponButton.isDrop) Drop();
        
    }

    private void PickUpButtonControl()
    {
         // Turn pn PickUp Button.        
        if(distanceToPlayer.magnitude <= gunPickUpRange)
        {
            PickUpButton.gameObject.SetActive(true);
        }
        else
        {
            PickUpButton.gameObject.SetActive(false);
        }

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
        this.gameObject.SetActive(true);
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
       this.gameObject.SetActive(false);
    }
}
