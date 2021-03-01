using UnityEngine;
using UnityEngine.UI;

public class LevelObjective : MonoBehaviour
{
    [SerializeField] GameObject objective;
    [SerializeField] Text ObjectiveText;
    [SerializeField] int enemyiesToKill;

    private void Start()
    {
        objective.SetActive(true);
    }
    private void Update()
    {
        ObjectiveText.text = "Kill atleast " + enemyiesToKill + " and reach finish point !";
        Invoke("SetObjectiveToFalse", 20f);
    }

    private void SetObjectiveToFalse()
    {
        objective.SetActive(false);
    }
}
