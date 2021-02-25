using UnityEngine;

public class SelectLevel : MonoBehaviour
{
  public void LevelButton(string level)
  {
      FindObjectOfType<SceneFader>().FadeTo(level);
  }
}
