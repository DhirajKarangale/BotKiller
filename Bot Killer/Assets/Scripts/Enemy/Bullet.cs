using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float impactForce;
    [SerializeField] Attack_Follow attack_Follow;

    private Transform player;
    private Vector3 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player.position;
     }
    
    private void Update()
    {
        transform.forward = target.normalized;
        transform.position = Vector3.MoveTowards(transform.position,target,(speed * Time.deltaTime));
        RaycastHit hit;
        Physics.Raycast(transform.position,target,out hit);
        if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
            player.position =player.position - hit.normal;
        }
        if(transform.position == target)
         Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
