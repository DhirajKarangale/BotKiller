using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] byte bulletForce;
    [SerializeField] float impactForce;
    [SerializeField] int damage;
    [SerializeField] Vector3 impactPosition;
    private PlayerMovement player;
    private Health_Dye_Trigger playerDye;
    private Vector3 target;
    private RaycastHit hit;


    private void Start()
    {
        playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
     }
    
    private void Update()
    {
        target = player.transform.position - transform.position;
        transform.forward = target.normalized;
        gameObject.GetComponent<Rigidbody>().AddForce(target.normalized * bulletForce,ForceMode.Impulse);
        
        Physics.Raycast(transform.position,target,out hit);
        if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           if(player != null)
           { 
               playerDye.TakeDamage(damage);
               player.transform.position =player.transform.position - (hit.normal + impactPosition);
               Destroy(gameObject,0.1f);
           }
        }
        else if(collision.gameObject.tag == "Sorrounding")
        {
            Destroy(gameObject);
        }
    }
}
