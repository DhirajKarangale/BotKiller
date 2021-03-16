using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Option()
    {
        SceneManager.LoadScene(2);
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
