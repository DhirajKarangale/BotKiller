using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] AnimationCurve animationCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
      StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t =1f;
        while (t > 0f)
        {
           t -= Time.deltaTime;
           float a = animationCurve.Evaluate(t);
           image.color = new Color(0f,0f,0f,a); 
           yield return 0;
        }
    } 

     IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        while (t < 1f)
        {
           t += Time.deltaTime;
           float a = animationCurve.Evaluate(t);
           image.color = new Color(0f,0f,0f,a); 
           yield return 0;
        }

        SceneManager.LoadScene(scene);
    } 
}
