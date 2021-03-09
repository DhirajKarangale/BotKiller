using UnityEngine;

public class GameComplete : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        FindObjectOfType<SceneFader>().FadeTo(scene);
    }
}
