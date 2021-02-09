using UnityEngine;
using System.Collections.Generic;

public class ItemsDestroy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject crackedItem;
    [SerializeField] List <GameObject> itemToDrop = new List<GameObject>();
    [SerializeField] Vector3 itemDropPosition;
   
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
       Instantiate(crackedItem,transform.position,transform.rotation);
       for(int i=0;i<itemToDrop.Count;i++)
       {
          Instantiate(itemToDrop[i],transform.position + itemDropPosition,transform.rotation);
       }
       Destroy(gameObject);
    }
}
