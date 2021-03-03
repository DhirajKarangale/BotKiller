using UnityEngine;
using UnityEngine.UI;
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

    [Header("LevelObjective")]
    public byte enemyDestroyed = 0;
    [SerializeField] GameObject objective;
    [SerializeField] Text ObjectiveText;
    [SerializeField] int enemyiesToKill;
    [SerializeField] Health_Death[] enemydye;


    private PlayerMovement player;
   private int currentScene;

   private void Start()
   {
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
       playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();

        objective.SetActive(true);
    }

   private void Update()
   {
     // Current Scene.
     currentScene =  SceneManager.GetActiveScene().buildIndex;

     if(!playerDye.isPlayerAlive)
     {
         Time.timeScale = 0.45f;  
         Invoke("SetGameOverScreenActive",0.5f);
         UIScreen.SetActive(false);
         GunContainer.SetActive(false);
         itemToIntroduce.SetActive(false);
         firstCamRef.SetActive(false);
         cam2.SetActive(true);
         fps.SetActive(false);
         player.enabled = false;
         playerDye.enabled = false;
     }
     else if(playerDye.isPlayerAlive && !weaponButton.isPaussed && !playerDye.isPlayerHitToFinish)
     {
       Time.timeScale = 1f; 
       gameOverScreen.SetActive(false);
     }

       if((enemyiesToKill == enemyDestroyed) && playerDye.isPlayerHitToFinish)
       {
            Debug.Log("Level Complete !");
            Time.timeScale = 0.5f;
            for(int i=0;i<enemydye.Length;i++)
            {
                if(enemydye[i] != null)
                {
                    enemydye[i].DestroyEnemy();
                }
            }

       }
        ObjectiveText.text = "Kill atleast " + enemyiesToKill + " and reach finish point !";
        Invoke("SetObjectiveToFalse", 20f);
    }

    private void SetObjectiveToFalse()
    {
        objective.SetActive(false);
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
