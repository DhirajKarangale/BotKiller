using TMPro;
using UnityEngine;

public class ScensitivityCountText_Save : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI sensitivityCountText;
   [SerializeField] OptionMenu optionMenu;

   
   private void Update()
   {
       if(sensitivityCountText != null)
       {
          sensitivityCountText.SetText((int)(optionMenu.sensitivityPercentage) + "%");
       }
   }

    public void SaveButton()
   {
     PlayerPrefs.SetFloat("LookSensitivity",optionMenu.lookSensitivity);
    
   }
}
