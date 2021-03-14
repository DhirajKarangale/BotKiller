using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] popups;
    private int popUpIndex;

    private void Update()
    {
        for(int i=0;i<popups.Length; i++)
        {
            if (i == popUpIndex) popups[popUpIndex].SetActive(true);
            else popups[popUpIndex].SetActive(false);
        }

        if(popUpIndex == 0)
        {
            if (Input.GetMouseButtonDown(0)) popUpIndex++;

        }
    }
}
