using UnityEngine.SceneManagement;
using UnityEngine;

public class Escape : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Option Menu");
        }
    }
}
