using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] float startTimeBetwwenMonster;
    private float timeBetweenMonster;

    [SerializeField] int numberOfMonster;

    private void Update()
    {
        if((timeBetweenMonster <= 0) && (numberOfMonster>0))
        {
            Instantiate(monster, transform.position, Quaternion.identity);
            timeBetweenMonster = startTimeBetwwenMonster;
            numberOfMonster--;
        }
        else
        {
            timeBetweenMonster -= Time.deltaTime;
        }
    }
}
