using UnityEngine;

public class Petroling : MonoBehaviour
{
    [SerializeField] byte speed;
    private float waitTime;
    [SerializeField] float startWaitTime;
    
    [SerializeField] Transform movePoint;
    [SerializeField] int minX;
    [SerializeField] int maxX;
    [SerializeField] int minY;
    [SerializeField] int maxY;
    [SerializeField] int minZ;
    [SerializeField] int maxZ;
     
    public bool isPetroling;

    private void Start()
    {
        waitTime = startWaitTime;

        // Setting move point first time.
        movePoint.position = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),Random.Range(minZ,maxZ));
        isPetroling =true;
    }

    private void Update()
    {
        if(isPetroling)
        {
            StartPetroling();
        }
    }

    private void StartPetroling()
    {
        // Setting Enemy Position .
        transform.position = Vector3.MoveTowards(transform.position,movePoint.position,(speed * Time.deltaTime));
        
        if(Vector3.Distance(transform.position,movePoint.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                 // Setting move point every time.
                  movePoint.position = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),Random.Range(minZ,maxZ));
                  waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
