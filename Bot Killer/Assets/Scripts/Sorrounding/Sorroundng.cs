using UnityEngine;

public class Sorroundng : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3 throwAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(player != null)
            {
                player.transform.position = player.transform.position + throwAmount;
            }
        }
    }
}
