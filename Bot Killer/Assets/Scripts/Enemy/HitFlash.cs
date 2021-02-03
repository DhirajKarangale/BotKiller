using UnityEngine;

public class HitFlash : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision Sucess");
        if(collision.gameObject.tag == "Bullet")
        {
           print("Bullet Collision Sucess");
        }
    }
}
