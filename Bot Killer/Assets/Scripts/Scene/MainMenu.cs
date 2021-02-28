using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play_Option(string sceneToLoad)
    {
      FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void Quit()
    {
         Application.Quit();
    }
}
