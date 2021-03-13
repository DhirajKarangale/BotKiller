using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private Health_Dye_Trigger playerDye;

    [Header("ToDesaible")]
   [SerializeField] WeaponButton weaponButton;
   [SerializeField] GameObject gameOverScreen;
   [SerializeField] GameObject UIScreen;
   [SerializeField] GameObject GunContainer;
   [SerializeField] GameObject itemToIntroduce;
   [SerializeField] GameObject firstCamRef;
   [SerializeField] GameObject cam2;
   [SerializeField] GameObject fps;

    [Header("ReSpwan Player")]
    [SerializeField] GameObject respWanEffect;
    [SerializeField] float effectArea;
    [SerializeField] int damage;
    [SerializeField] float explosionForce;

    private Player player;
   private int currentScene;

   private void Start()
   {
        player.enabled = true;
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
        gameOverScreen.SetActive(false);
   }

   private void Update()
   {
     // Current Scene.
     currentScene =  SceneManager.GetActiveScene().buildIndex;

     if(!playerDye.isPlayerAlive)
     {
            PlayerDye();
     }
     else if(playerDye.isPlayerAlive && !weaponButton.isPaussed && !playerDye.isPlayerHitToFinish)
     {
       Time.timeScale = 1f; 
       gameOverScreen.SetActive(false);
     }

        if (Input.GetKey(KeyCode.Escape))
        {
            weaponButton.PauseButton();
        }
    }

    private void PlayerDye()
    {
        Time.timeScale = 0.8f;
        Invoke("SetGameOverScreenActive", 1f);
        UIScreen.SetActive(false);
        GunContainer.SetActive(false);
        itemToIntroduce.SetActive(false);
        firstCamRef.SetActive(false);
        cam2.SetActive(true);
        fps.SetActive(false);
        player.enabled = false;
        playerDye.enabled = false;
    }

    public void PlayerReSpwan()
    {
        playerDye.isPlayerAlive = true;
        Time.timeScale = 1f;
        Invoke("SetGameOverScreenActive", 1f);
        UIScreen.SetActive(true);
        GunContainer.SetActive(true);
        itemToIntroduce.SetActive(true);
        firstCamRef.SetActive(true);
        cam2.SetActive(false);
        fps.SetActive(true);
        player.enabled = true;
        playerDye.enabled = true;
        playerDye.currentHealth = playerDye.health;
        GameObject currentRespwanEffect = Instantiate(respWanEffect, player.transform.position, player.transform.rotation);
        Destroy(currentRespwanEffect, 10f);
        ThrowAndDamegeItem();
    }

    private void ThrowAndDamegeItem()
    {
        Collider[] colliderToMove = Physics.OverlapSphere(transform.position, effectArea); // Finding the object near granide to move them.
        foreach (Collider nearByObject in colliderToMove)
        {
            Rigidbody rb = nearByObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, effectArea); // Adding force to object
            }
        }

        Collider[] colliderToDestroy = Physics.OverlapSphere(transform.position, effectArea); // Finding the object near granide to destroy them.
        foreach (Collider nearByObject in colliderToDestroy) // Go through all object
        {
            ItemsDestroy item = nearByObject.GetComponent<ItemsDestroy>(); // Finding item to destroy 
            if (item != null)
            {
                item.TakeDamage(damage); // Damage Item
            }

            Health_Death enemyDye = nearByObject.GetComponent<Health_Death>(); // Finding enemy to destroy.
            if (enemyDye != null)
            {
                enemyDye.TakeDamage(damage); // Damage Enemy.
            }
        }
    }
        
    private void SetGameOverScreenActive()
   {
     gameOverScreen.SetActive(true);
   }

   public void MainMenuButton(string sceneToLoad)
   {
     FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
   }

   public void RestartButton()
   {
      SceneManager.LoadScene(currentScene);
   }
       
}
