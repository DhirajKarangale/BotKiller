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
       if(crackedItem != null)
       {
          GameObject currentCrackedItem = Instantiate(crackedItem,transform.position + crackedItemDropPosition,Quaternion.identity);
          Destroy(currentCrackedItem,20f);
       }
        GameObject currentCrackedEffect = Instantiate(crackedEffect,transform.position + crackedItemDropPosition,crackedItem.transform.rotation);
        Destroy(currentCrackedEffect,2f);
       for(int i=0;i<itemToDrop.Count;i++)
       {
        itemToDrop[i].SetActive(true);
       }
       Destroy(gameObject);
    }
}
