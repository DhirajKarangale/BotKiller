using UnityEngine;

public class Sorroundng : MonoBehaviour
{
    private Follow_Attack enemy;
    [SerializeField] Player player;
    [SerializeField] Vector3 throwAmount;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Follow_Attack>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player collided to wall");
            player.transform.position = player.transform.position + throwAmount;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy collided to wall");
            enemy.transform.position = enemy.transform.position + throwAmount;
        }
    }
}
