using UnityEngine;
using System.Collections.Generic;

public class ItemsDestroy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject crackedItem;
    [SerializeField] GameObject crackedEffect;
    [SerializeField] Vector3 itemDropPosition,crackedItemDropPosition;
    [SerializeField] List <GameObject> itemToDrop = new List<GameObject>();
  
    
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
       Instantiate(crackedItem,transform.position + crackedItemDropPosition,transform.rotation);
       Instantiate(crackedEffect,transform.position + crackedItemDropPosition,crackedItem.transform.rotation);
       for(int i=0;i<itemToDrop.Count;i++)
       {
          Instantiate(itemToDrop[i],transform.position + itemDropPosition,transform.rotation);
       }
       Destroy(gameObject);
    }
}
