using UnityEngine;
using UnityEngine.UI;


public class OptionMenu : MonoBehaviour
{
    public Slider slider;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public float lookSensitivity;
    public float sensitivityPercentage;
    public int currentQualityIndex;
    
    private void Start()
    {
        slider.minValue = 100;
        slider.maxValue = 1000f;
        slider.wholeNumbers = true;

        slider.value = lookSensitivity;

         lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity");
         currentQualityIndex = PlayerPrefs.GetInt("Quality");
         QualitySettings.SetQualityLevel(currentQualityIndex);
         resolutionDropdown.value = currentQualityIndex;
    }
  
   public void SetGraphics(int qualityIndex)
   {
       currentQualityIndex = qualityIndex;
       QualitySettings.SetQualityLevel(currentQualityIndex);
   }

   public void Button(string sceneToLoad)
   {
       FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
   }

   public void SensitivitySlider(float value)
   {
     lookSensitivity = value; 
     sensitivityPercentage = (value/1000f) * 100f;
   }

   public void SaveButton()
   {
      PlayerPrefs.SetInt("Quality",currentQualityIndex);
     PlayerPrefs.Save();
   }
}
