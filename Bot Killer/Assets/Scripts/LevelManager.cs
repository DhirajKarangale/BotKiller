using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("LevelComplete")]
    [SerializeField] GameObject levelCompleteScreen;
    [SerializeField] Health_Death[] enemydye;
    [SerializeField] GameObject winParicleEffect;
    [SerializeField] Player player;
    public GameObject objective;
    [SerializeField] Text ObjectiveText;
    public int enemyiesToKill;
    [SerializeField] GameObject crossHair;
    private Health_Dye_Trigger playerDye;
    public byte enemyDestroyed = 0;
    private bool finishAllowed;
    private int nextLevel;

    private void Start()
    {
        player.enabled = true;
        finishAllowed = true;
        Time.timeScale = 1f;
        levelCompleteScreen.SetActive(false);
        objective.SetActive(true);
        playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void Update()
    {
        ObjectiveText.text = "Kill atleast " + enemyiesToKill + " and reach finish point !";
        Invoke("SetObjectiveToFalse", 20f);


        if ((enemyDestroyed >= enemyiesToKill) && playerDye.isPlayerHitToFinish && finishAllowed)
        {
            if(SceneManager.GetActiveScene().buildIndex != 13)
            {
                if (nextLevel > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextLevel);
                }
            }
           
            finishAllowed = false;
            objective.SetActive(false);
            GameObject winParticle = Instantiate(winParicleEffect, playerDye.transform.position, Quaternion.identity);
            Destroy(winParticle, 15f);
            Invoke("LevelCompleteScreenSetActive", 0.5f);
            for (int i = 0; i < enemydye.Length; i++)
            {
                if (enemydye[i] != null)
                {
                    enemydye[i].DestroyEnemy();
                }
            }
            Time.timeScale = 0.5f;
        }
    }

    private void LevelCompleteScreenSetActive()
    {
        levelCompleteScreen.SetActive(true);
        crossHair.SetActive(false);
        player.enabled = false;
    }

    private void SetObjectiveToFalse()
    {
        objective.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenuButton(string sceneToLoad)
    {
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }
}
