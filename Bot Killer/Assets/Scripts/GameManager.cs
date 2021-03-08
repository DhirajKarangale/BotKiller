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

    private PlayerMovement player;
   private int currentScene;

   private void Start()
   {
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
        gameOverScreen.SetActive(false);
   }

   private void Update()
   {
     // Current Scene.
     currentScene =  SceneManager.GetActiveScene().buildIndex;

     if(!playerDye.isPlayerAlive)
     {
         Time.timeScale = 0.8f;  
         Invoke("SetGameOverScreenActive",1f);
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
