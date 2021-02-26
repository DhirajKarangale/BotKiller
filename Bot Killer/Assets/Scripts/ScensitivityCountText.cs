using TMPro;
using UnityEngine;

public class ScensitivityCountText : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI sensitivityCountText;
   [SerializeField] OptionMenu optionMenu;

   private void Update()
   {
       if(sensitivityCountText != null)
       {
          sensitivityCountText.SetText(optionMenu.sensitivityPercentage + "%");
       }
   }
}
