using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMPro.TMP_Dropdown dropdown;
    [SerializeField] Text sensitivityCountText;
    public float lookSensitivity;
    private float currentlookSensitivity;
    private int qualityIndex,currentQualityIndex;

    private void Start()
    {
        // Slider
        // Setting Slider Value
        slider.minValue = 100f;
        slider.maxValue = 1000f;
        slider.wholeNumbers = true;

        currentlookSensitivity = PlayerPrefs.GetFloat("LookSensitivity",140f); // Taking Previously saved slider Valve or Deafult 140.
        lookSensitivity = currentlookSensitivity;
        slider.value = lookSensitivity; // Setting Saved Slider valve.
        sensitivityCountText.text = (int)((lookSensitivity/1000) *100) + "%"; // Setting Saved Text of Sensitivity count.

        
        // Dropdown
        currentQualityIndex = PlayerPrefs.GetInt("QualitySetting",3);
        qualityIndex = currentQualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
        dropdown.value = qualityIndex;
    }

    private void Update()
    {
        slider.value = lookSensitivity; // Updating Slider valve.
        sensitivityCountText.text = (int)((lookSensitivity/1000) *100) + "%"; // Updating Text of Sensitivity count.
    }
    
    // Buttons.
    public void SceneButton(string sceneToLoad)
    {
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    // Sensitivity Slider.
    public void SensitivitySlider(float value)
    {
        lookSensitivity = value; // Changing Sensitivity Value with slider.
    }

    // Graphics Dropdown
    public void GraphicsDropdown(int value)
    {
        qualityIndex = value;
        QualitySettings.SetQualityLevel(qualityIndex); // Changing quality with dropdown.
    }

    // Save Button
    public void SaveButton()
    {
        PlayerPrefs.SetFloat("LookSensitivity",lookSensitivity); // Saving Sensitivity Value.
        PlayerPrefs.SetInt("QualitySetting",qualityIndex); // Saving quality Value.
    }
}
