using UnityEngine;

public class Attack_Follow : MonoBehaviour
{
  [SerializeField] byte speed;
  [SerializeField] byte followRange;
  [SerializeField] byte attackRange;
  [SerializeField] byte stopDistance;
  [SerializeField] byte retriveDistance;

  [SerializeField] Transform player;
  [SerializeField] Petroling petroling;

  private void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
  }   

  private void Update()
  {
      // Follow to player.
    if((Vector3.Distance(transform.position,player.position)<followRange) && (Vector3.Distance(transform.position,player.position)>stopDistance))
    {
       transform.position = Vector3.MoveTowards(transform.position,player.position,(speed * Time.deltaTime));
       petroling.isPetroling = false;
    }
    else if((Vector3.Distance(transform.position,player.position)<followRange) && (Vector3.Distance(transform.position,player.position)<=stopDistance) && (Vector3.Distance(transform.position,player.position)>=retriveDistance))
    {
        transform.position = this.transform.position;
        petroling.isPetroling = false;
    }
    else if((Vector3.Distance(transform.position,player.position)<followRange) && (Vector3.Distance(transform.position,player.position)<followRange))
    {
        transform.position = Vector3.MoveTowards(transform.position,player.position,(-speed * Time.deltaTime));
        petroling.isPetroling = false;
    }
    else if(Vector3.Distance(transform.position,player.position)>followRange)
    {
        petroling.isPetroling = true;
    }
   
  }
}
