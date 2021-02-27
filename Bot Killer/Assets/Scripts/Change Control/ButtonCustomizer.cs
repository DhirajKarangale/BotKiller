using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ButtonCustomizer : MonoBehaviour
{
  [SerializeField] Slider sizeSlider;
  [SerializeField] Slider opcitySlider;
  [SerializeField] Text sizeCountText;
  [SerializeField] Text opicityCountText;
  public UIDrag selectButton;
  private UIDrag[] buttons;
  private bool isExit;

  private void Start()
  {
    buttons = GetComponentsInChildren<UIDrag>();
  }

  private void Update()
  {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
   if(currentScene == 3)
   {
    sizeCountText.text = (int)(sizeSlider.value * 100) + "%";
    opicityCountText.text = (int)(opcitySlider.value * 100) + "%";
    if(selectButton)
    {
        selectButton.SetSizeAndOpicity(sizeSlider.value,opcitySlider.value);
    }
   }
  }

  public void SetButtonData(float size,float opci)
  {
      sizeSlider.value = size;
      opcitySlider.value = opci;
  }

  public void SaveData()
  {
      foreach (var b in buttons)
      {
          b.SaveData();
      }
  }

   public void ResetData()
  {
      foreach (var b in buttons)
      {
          b.ResetUI();
      }
  }

  public void Exit(string sceneToLoad)
  {
      FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
  }
}
