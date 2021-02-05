using UnityEngine;

public class CamShake : MonoBehaviour
{
  [SerializeField] Animator camAnim;

  public void Shake()
  {
      camAnim.SetTrigger("Shake");
  }    
}
