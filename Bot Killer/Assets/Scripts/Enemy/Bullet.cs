using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] byte speed;
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
        target = player.transform.position;
     }
    
    private void Update()
    {
        transform.forward = target.normalized;
        transform.position = Vector3.MoveTowards(transform.position,target,(speed * Time.deltaTime));
        
        Physics.Raycast(transform.position,target,out hit);
        if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
        if(transform.position == target)
        Destroy(gameObject,0.1f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           if(player != null)
           { 
               playerDye.TakeDamage(damage);
           }
            Destroy(gameObject,0.1f);
            player.transform.position =player.transform.position - (hit.normal + impactPosition);
        }
    }
}
