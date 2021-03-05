using UnityEngine;
using UnityEngine.UI;

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

    public void LevelButton(string level)
  {
      FindObjectOfType<SceneFader>().FadeTo(level);
  }
}
