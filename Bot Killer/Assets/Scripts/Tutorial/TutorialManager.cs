using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] popups;
    private int popUpIndex;
    private float timeBetweenPopUps = 3f;
    private bool isContinue;

    [Header("Refrences")]
    [SerializeField] Player player;
    [SerializeField] WeaponButton weaponButton;

    private void Update()
    {
        for(int i=0;i<popups.Length; i++)
        {
            if (i == popUpIndex)
            {
                popups[popUpIndex].SetActive(true);
            }
        }

        if(popUpIndex == 0)
        {
           if (player.isMoving)
           {
                if((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
            
        }
        else if(popUpIndex == 1)
        {
            if (player.isRotating)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 2)
        {
            if (weaponButton.isThrust)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 3)
        {
            if (weaponButton.shotting)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 4)
        {
            if (weaponButton.isScope)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 5)
        {
            if (weaponButton.isReload)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 6)
        {
            if (weaponButton.isPickUp)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 7)
        {
            if (weaponButton.isPickUp)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 3f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 8)
        {
            if (isContinue && (timeBetweenPopUps <= 0))
            {
                popups[popUpIndex].SetActive(false);
                popUpIndex++;
                timeBetweenPopUps = 3f;
                isContinue = false;
            }
            else
            {
                timeBetweenPopUps -= Time.deltaTime;
            }
        }
    }

    public void ContinueButton()
    {
        isContinue = true;
    }
}
