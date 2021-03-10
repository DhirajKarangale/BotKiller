using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Vector3 impactPosition;
    private Player player;
    private Health_Dye_Trigger playerDye;


    private void Start()
    {
        playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
  
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           if(player != null)
           { 
               player.transform.position = player.transform.position - impactPosition;
               playerDye.TakeDamage(damage);
               Destroy(gameObject,0.01f);
           }
        }
        if(collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
