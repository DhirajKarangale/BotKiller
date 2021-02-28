using UnityEngine;


public class WeaponButton : MonoBehaviour
{ 
    [SerializeField] GameObject pauseScreen;
    public bool shotting,isReload,isPickUp,isDrop,isScope,throwGranide,isThrust,isPaussed;

    private void Start()
    {
      isPaussed = false;
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
    }

    public void ThrustPointerDown()
    {
        isThrust = true;
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
     isPaussed = true;
     Time.timeScale = 0f;
   }

   public void ResumeButtom()
   {
     isPaussed = false;
     Time.timeScale = 1f;
   }

   public void SceneButton(string scene)
  {
    FindObjectOfType<SceneFader>().FadeTo(scene);
    Time.timeScale = 1f;
    pauseScreen.SetActive(false);
  }
}
