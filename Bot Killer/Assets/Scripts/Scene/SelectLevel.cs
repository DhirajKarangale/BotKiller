using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;

    private void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 4);

        for(int i=0;i<levelButtons.Length;i++)
        {
            if(i+4>levelAt)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void LevelButton(string level)
    { 
      FindObjectOfType<SceneFader>().FadeTo(level);
    }
}
