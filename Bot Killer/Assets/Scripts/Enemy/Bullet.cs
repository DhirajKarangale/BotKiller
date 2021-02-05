using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] byte speed;
    [SerializeField] float impactForce;
    [SerializeField] int damage;
    [SerializeField] Vector3 impactPosition;
    private Player players;
    private Transform player;
    private Vector3 target;
    RaycastHit hit;

    private void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player.position;
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           if(players != null)
           { 
               players.TakeDamage(damage);
           }
            Destroy(gameObject);
            player.position =player.position - (hit.normal + impactPosition);
        }
    }
}
