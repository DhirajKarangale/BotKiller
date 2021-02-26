using UnityEngine;
using UnityEngine.UI;


public class OptionMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    public float lookSensitivity;
    public float sensitivityPercentage;
    
    private void Start()
    {
        slider.minValue = 100;
        slider.maxValue = 1000f;
        slider.wholeNumbers = true;
    }
  
   public void SetGraphics(int qualityIndex)
   {
       QualitySettings.SetQualityLevel(qualityIndex);
   }

   public void BackButton(string sceneToLoad)
   {
       FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
   }

   public void SensitivitySlider(float value)
   {
     sensitivityPercentage = (value/1000f) * 100f;
     lookSensitivity = value; 
   }
}
