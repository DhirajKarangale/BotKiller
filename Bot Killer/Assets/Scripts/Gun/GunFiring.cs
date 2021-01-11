using TMPro;
using UnityEngine;

public class GunFiring : MonoBehaviour
{
    AudioSource audioSource;

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
 
   GameObject currentBullet;

    public void PointerUp()
    {
        shotting = false;
    }

    public void PointerDown()
    {
        shotting = true;
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;     
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        // Ammo Display.
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft + " / " + magazineSize);
        }
      
        // Automatically Reload.
        if (!reloading && (bulletsLeft <= 0))
        {
            Reload();
        }

        // reload.
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (shotting)
        {
            if (readyToShoot && shotting && !reloading && (bulletsLeft > 0))
            {
                Shoot();
            }

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

        if (readyToShoot && !reloading && (bulletsLeft > 0))
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
        audioSource.Play();
        readyToShoot = false;
        RaycastHit hit;
        Ray ray = fpscamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Setting target posotion to middle of screen.



        // Checking what ray is hitting.
         Vector3 targetPoint;
  
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
          
           ItemsDestroy item =  hit.transform.GetComponent<ItemsDestroy>();
            if(item !=null)
            {
                item.TakeDamage(damage);
                Destroy(currentBullet);
            }
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
         currentBullet = Instantiate(bullets, attackPoint.position, Quaternion.identity);

        // Rotate Bullets to Shoot Direction.
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        // Add force to bullet.
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpscamera.transform.up * upwardForce , ForceMode.Impulse);

        // Destroy Bullets Automatically.
        Destroy(currentBullet, 5f);

      

        if (muzzelFlash != null)
        {
          GameObject currentMuzzelFlash = Instantiate(muzzelFlash, attackPoint.position, Quaternion.identity);
            Destroy(currentMuzzelFlash, 5f);
          
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
