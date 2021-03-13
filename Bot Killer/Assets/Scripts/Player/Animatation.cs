using UnityEngine;

public class Animatation : MonoBehaviour
{

    public bool reloading;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] Animator animator;
    private Player player;
    private Health_Dye_Trigger playerDye;
    private Vector3 playerPos;
    private void Start()
    {
        playerDye = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Dye_Trigger>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPos = player.transform.position;
    }

    private void Update()
    {
        if (playerDye.isPlayerAlive)
        {
            if (weaponButton.shotting && (playerPos != player.transform.position))
            {
                animator.SetBool("OriginalPos", true);
                animator.SetBool("Breath", false);
                animator.SetBool("Reload", false);
                animator.SetBool("Runing", false);
                animator.SetBool("Scoped", false);
            }


            if ((playerPos == player.transform.position) && (reloading == false) && (weaponButton.shotting == false))
            {
                animator.SetBool("Breath", true);
                animator.SetBool("Reload", false);
                animator.SetBool("Runing", false);
                animator.SetBool("OriginalPos", false);
                animator.SetBool("Scoped", false);
            }

            if ((weaponButton.shotting == false) && (reloading == false) && (playerPos != player.transform.position) && !weaponButton.isThrust)
            {
                animator.SetBool("Breath", false);
                animator.SetBool("Reload", false);
                animator.SetBool("Runing", true);
                animator.SetBool("OriginalPos", false);
                animator.SetBool("Scoped", false);
            }

            if (reloading == true)
            {
                animator.SetBool("Scoped", false);
                animator.SetBool("Breath", false);
                animator.SetBool("Reload", true);
                animator.SetBool("Runing", false);
                animator.SetBool("OriginalPos", false);
            }
           else if (weaponButton.isScope)
            {
                animator.SetBool("Breath", false);
                animator.SetBool("Reload", false);
                animator.SetBool("Runing", false);
                animator.SetBool("OriginalPos", false);
                animator.SetBool("Scoped", true);
            }
            playerPos = player.transform.position;
        }


    }
}
