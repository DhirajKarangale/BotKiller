using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] GameObject bullets;
    [SerializeField] Transform gunPosotion;

    private void Update()
    {
        firingBullets();
    }

    private void firingBullets()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            firingBulletsButton();
        }
    }

    public void firingBulletsButton()
    {
        GameObject fireBullets = Instantiate(bullets);
    }
}
