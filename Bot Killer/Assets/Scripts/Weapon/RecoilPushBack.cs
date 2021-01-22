using UnityEngine;

public class RecoilPushBack : MonoBehaviour
{
   [Header("Recoiler Position")]
   [SerializeField] Transform PushBackRecoilPositionPoint;
   [SerializeField] Transform RotationRecoilPositionPoint;
   [Header("Recoil Setting")]
   [SerializeField] float PositionDampTime;
   [SerializeField] float RotationDampTime;
   [SerializeField] float BackForce;
   [Header("Recoil")]
   [SerializeField] Vector3 RotationRecoil;
   [SerializeField] Vector3 PushBackRecoil;

   private Vector3 CurrentRotationRecoil;
   private Vector3 CurrentPushBackRecoil;
   private Vector3 RotationOutput;


  void FixedUpdate()
   {
      CurrentRotationRecoil = Vector3.Lerp(CurrentRotationRecoil,Vector3.zero,BackForce*Time.deltaTime);
      CurrentPushBackRecoil = Vector3.Lerp(CurrentPushBackRecoil,Vector3.zero,BackForce*Time.deltaTime);

      PushBackRecoilPositionPoint.localPosition = Vector3.Slerp(PushBackRecoilPositionPoint.localPosition,CurrentPushBackRecoil,PositionDampTime*Time.fixedDeltaTime);
      RotationOutput = Vector3.Slerp(RotationOutput,CurrentRotationRecoil,RotationDampTime*Time.fixedDeltaTime);
      RotationRecoilPositionPoint.localRotation = Quaternion.Euler(RotationOutput);
   }
public void Fire()
{
    CurrentRotationRecoil += new Vector3(RotationRecoil.x,RotationRecoil.y,RotationRecoil.z);
    CurrentPushBackRecoil += new Vector3(PushBackRecoil.x,PushBackRecoil.y,PushBackRecoil.z);
}
}
