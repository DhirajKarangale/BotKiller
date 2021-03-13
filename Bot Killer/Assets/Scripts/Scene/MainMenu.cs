using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject QuitButtonPanel;

    private void Start()
    {
        QuitButtonPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            QuitButtonPanel.SetActive(true);
        }
    }

    public void Play_Option(string sceneToLoad)
    {
      FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void Quit()
    {
         Application.Quit();
    }

    public void NoButton()
    {
        QuitButtonPanel.SetActive(false);
    }


}
