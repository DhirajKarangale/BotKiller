using UnityEngine;
using System.Collections.Generic;

public class ItemsDestroy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject crackedItem;
    [SerializeField] GameObject crackedEffect;
    [SerializeField] Vector3 itemDropPosition,crackedItemDropPosition;
    [SerializeField] GameObject itemToDrop;
    [SerializeField] List <GameObject> itemToActive = new List<GameObject>();
  
    
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
       if(crackedItem != null)
       {
          GameObject currentCrackedItem = Instantiate(crackedItem,transform.position + crackedItemDropPosition,Quaternion.identity);
          Destroy(currentCrackedItem,120f);
       }
        GameObject currentCrackedEffect = Instantiate(crackedEffect,transform.position + crackedItemDropPosition,crackedItem.transform.rotation);
        Destroy(currentCrackedEffect,2f);
        if(itemToDrop != null)
        {
            Instantiate(itemToDrop,transform.position + crackedItemDropPosition,Quaternion.identity);
        }
       for(int i=0;i<itemToActive.Count;i++)
       {
        itemToActive[i].SetActive(true);
       }
       Destroy(gameObject);
    }
}
