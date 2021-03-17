using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Slider speedSlider;
    [SerializeField] TMPro.TMP_Dropdown dropdown;
    [SerializeField] Text sensitivityCountText;
    [SerializeField] Text speedCountText;
    private float currentMoveSpeed;
    public static float lookSensitivity = 5f;
    private float currentlookSensitivity;
    private int qualityIndex,currentQualityIndex;

    private void Start()
    {
        // Slider
        // Setting Sensitivity Slider Value
        slider.minValue = 1f;
        slider.maxValue = 30f;
        slider.wholeNumbers = true;

        currentlookSensitivity = PlayerPrefs.GetFloat("LookSensitivity",5f); // Taking Previously saved slider Valve or Deafult 140.
        lookSensitivity = currentlookSensitivity;
        slider.value = lookSensitivity; // Setting Saved Slider valve.
        sensitivityCountText.text = (int)((lookSensitivity / 30) *100) + "%"; // Setting Saved Text of Sensitivity count.


        //Setting Speed Slider
        speedSlider.minValue = 1f;
        speedSlider.maxValue = 50f;
        speedSlider.wholeNumbers = true;

        currentMoveSpeed = PlayerPrefs.GetFloat("MoveSpeed",20f); // Taking Previously saved slider Valve or Deafult 140.
        Player.moveSpeed = currentMoveSpeed;
        speedSlider.value = Player.moveSpeed; // Setting Saved Slider valve.
       speedCountText.text = (int)((Player.moveSpeed / 50) * 100) + "%"; // Setting Saved Text of Sensitivity count.



        // Dropdown
        currentQualityIndex = PlayerPrefs.GetInt("QualitySetting",3);
        qualityIndex = currentQualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
        dropdown.value = qualityIndex;

    }

    private void Update()
    {
        slider.value = lookSensitivity; // Updating Slider valve.
        sensitivityCountText.text = (int)((lookSensitivity/30) *100) + "%"; // Updating Text of Sensitivity count.

        speedSlider.value = Player.moveSpeed;
        speedCountText.text = (int)((Player.moveSpeed / 50) * 100) + "%";

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
    
    // Buttons.
    public void ChangeColtrol(string sceneToLoad)
    {
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    // Sensitivity Slider.
    public void SensitivitySlider(float value)
    {
        lookSensitivity = value; // Changing Sensitivity Value with slider.
    }

    public void SpeedSlider(float value)
    {
        Player.moveSpeed = value; // Changing Sensitivity Value with slider.
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
        PlayerPrefs.SetFloat("MoveSpeed", Player.moveSpeed); // Saving Speed Value.
    }
}
