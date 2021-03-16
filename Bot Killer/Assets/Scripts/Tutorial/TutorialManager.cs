using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] popups;
    private int popUpIndex;
    private float timeBetweenPopUps = 3f;
    private int saveRef = 0;

    [Header("Refrences")]
    [SerializeField] Player player;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] GameObject enemyHoleObject;
    [SerializeField] Gun gun;
    [SerializeField] PickUpController pickUpController;

    private void Start()
    {
        enemyHoleObject.SetActive(false);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("TutorialSave", 0) == 0)
        {
            Invoke("Tutorial", 2f);
        }
        else
        {
            enemyHoleObject.SetActive(true);
            weaponButton.enabled = true;
        }
    }

    private void Tutorial()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            if (i == popUpIndex)
            {
                popups[popUpIndex].SetActive(true);
            }
        }

        if (popUpIndex == 0)
        {
            if (player.isMoving)
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
        else if (popUpIndex == 1)
        {
            if (player.isRotating)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 1.8f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 2)
        {
            if (player.isPlayerFly)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 1.8f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }

        }
        else if (popUpIndex == 3)
        {
            if (gun.isPlayerShoot)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 1f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 4)
        {
            if (Gun.isScopeOn)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 1f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 5)
        {
            if (!Gun.isScopeOn)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 2f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 6)
        {
            if (gun.isGunReload)
            {
                if ((timeBetweenPopUps <= 0))
                {
                    popups[popUpIndex].SetActive(false);
                    popUpIndex++;
                    timeBetweenPopUps = 2f;
                }
                else
                {
                    timeBetweenPopUps -= Time.deltaTime;
                }
            }
        }
        else if (popUpIndex == 7)
        {
            if (pickUpController.isGunDrop)
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
            if (pickUpController.isGunPickUp)
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
        else if (popUpIndex == 9)
        {

            if (Input.GetMouseButtonDown(0) && (timeBetweenPopUps <= 0))
            {
                popups[popUpIndex].SetActive(false);
                popUpIndex++;
                timeBetweenPopUps = 3f;
                enemyHoleObject.SetActive(true);
                saveRef = 1;
                PlayerPrefs.SetInt("TutorialSave", saveRef);
            }
            else
            {
                timeBetweenPopUps -= Time.deltaTime;
            }
        }
    }

}

