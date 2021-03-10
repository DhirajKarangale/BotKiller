using UnityEngine;
using UnityEngine.SceneManagement;


public class WeaponButton : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject crossHair;
    public bool shotting,isReload,isPickUp,isDrop,isScope,throwGranide,isThrust,isPaussed;

    private void Start()
    {
      isPaussed = false;
        crossHair.SetActive(true);
    }

    private void Update()
    {
      if(isPaussed)
      {
        pauseScreen.SetActive(true);
      }
      else
      {
        pauseScreen.SetActive(false);
      }
    }

     public void ThrustPointerUp()
    {
        isThrust = false;
        if ((SceneManager.GetActiveScene().buildIndex != 3))
        {
            audioSource.Stop();
        }
    }

    public void ThrustPointerDown()
    {
        if((SceneManager.GetActiveScene().buildIndex != 3) && !Gun.isScopeOn)
        {
            isThrust = true;
            audioSource.Play();
        }
    }

    public void AttackButtonPointerUp()
    {
      shotting = false;
    }

    public void AttackButtonPointerDown()
    {
      shotting = true;
    }
        
    public void Reload()
    {
      isReload = true;
    }

     public void PickUpDropButtonPointerUP()
    {
      isPickUp = false;
    }

    public void PickUpDropButtonPointerDown()
    {
      isPickUp = true;
    }
    
   public void ScopeButton()
   {
     isScope = !isScope;
   }

   public void ThrowGranide()
   {
     throwGranide = true;
   }

   public void PauseButton()
   {
     if (SceneManager.GetActiveScene().buildIndex != 3)
     {
            isPaussed = true;
            Time.timeScale = 0f;
            crossHair.SetActive(false);
     }
   }

   public void ResumeButtom()
   {
     isPaussed = false;
     Time.timeScale = 1f;
        crossHair.SetActive(true);
    }

   public void SceneButton(string scene)
  {
    FindObjectOfType<SceneFader>().FadeTo(scene);
    Time.timeScale = 1f;
    pauseScreen.SetActive(false);
  }
}
