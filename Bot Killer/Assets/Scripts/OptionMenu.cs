using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
   public void SetGraphics(int qualityIndex)
   {
       QualitySettings.SetQualityLevel(qualityIndex);
   }

   public void BackButton(string sceneToLoad)
   {
       FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
   }
}
