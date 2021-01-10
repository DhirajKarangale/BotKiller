using TMPro;
using UnityEngine;

public class GunFiring : MonoBehaviour
{
    [SerializeField] GameObject bullets;

    [SerializeField] float shootForce, upwardForce;

    [SerializeField] int damage;
    [SerializeField] float timeBetweenShotting, spread, reloadTime, timeBetweenShots;
    [SerializeField] int magazineSize, bulletsPerTrap;
    [SerializeField] bool allowHoldButton;


    private int bulletsLeft, bulletsShots;

    private bool shotting, readyToShoot, reloading;

    [SerializeField] GameObject muzzelFlash;
    [SerializeField] TextMeshProUGUI ammoDisplay;

    [SerializeField] Camera fpscamera;
    [SerializeField] Transform attackPoint;

    [SerializeField] bool allowInvoke = true;

    bool isFiring;

    public void PointerUp()
    {
        isFiring = true;
    }

    public void PointerDown()
    {
        isFiring = false;
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;     
    }

    private void Update() 
    {
        if(isFiring)
        {
            MyInput();
        }
    }


   

    private void MyInput()
    {

        // Checking the hold button or not.         
        if(allowHoldButton)
        {
            shotting = Input.GetKey(KeyCode.Z);
        }
        else
        {
            shotting = Input.GetKeyDown(KeyCode.Z);
        }

        if (readyToShoot && shotting && !reloading && (bulletsLeft > 0))
        {
            Shoot();
        }

        // Automatically Reload.
        if (readyToShoot && shotting && !reloading && (bulletsLeft <= 0))
        {
            Reload();
        }

        // Ammo Display.
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft + " / " + magazineSize);
        }

        // reload.
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

    }



    public void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpscamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Setting target posotion to middle of screen.

        RaycastHit hit;

        // Checking what ray is hitting.
        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }
        


        // Calculatating direction between target point and attack point.
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // Calculate Spread.
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculatating direction between target point and attack point with spread.
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        // Instantiate bullets.
        GameObject currentBullet = Instantiate(bullets, attackPoint.position, Quaternion.identity);

        // Rotate Bullets to Shoot Direction.
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        // Add force to bullet.
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpscamera.transform.up * upwardForce , ForceMode.Impulse);

       
        // Destroy Bullets.
        Destroy(currentBullet, 5f);

        if(muzzelFlash != null)
        {
            Instantiate(muzzelFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsShots++;

        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShotting);  
            allowInvoke = false;

        }

    }

   

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }


    public void Reload()
    {
        reloading = true;
        Invoke("ReloadingFinish", reloadTime);
    }

    private void ReloadingFinish()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
