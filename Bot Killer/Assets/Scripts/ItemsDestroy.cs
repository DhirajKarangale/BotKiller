using UnityEngine;

public class ItemsDestroy : MonoBehaviour
{
    [SerializeField] int health = 100;

   public void TakeDamage(int damage)
    {
        health -= damage;
        if(health<=damage)
        {
            DestroyItem();
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
