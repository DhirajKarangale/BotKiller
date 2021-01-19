using UnityEngine;
using TMPro;


public class Gun : MonoBehaviour
{
    // Object in Script.
    private AudioSource audioSource;
    private GameObject currentBullet;
    private Vector3 directionOfAttackAndHitPoint;
    private Vector3 targetPoint;
    private RaycastHit hit;
    private Ray ray;

    [Header("Bullets Attributes")]
    [SerializeField] float shootForce;
    [SerializeField] float impactForce;
    [SerializeField] int damage;
    [SerializeField] float timeBetweenShotting, reloadTime, timeBetweenShots;
    [SerializeField] int magazineSize, bulletsPerTrap;
    private int bulletsLeft, bulletsShots;

    [Header("Prefab")]
    [SerializeField] GameObject bullets;
    [SerializeField] Recoil recoil;
    public WeaponButton weaponButton;
    public WeaponManager weaponManager;

    [Header("Display")]
    [SerializeField] GameObject muzzelFlash;
    [SerializeField] GameObject impactEffect;
    [SerializeField] TextMeshProUGUI ammoDisplay;
    [SerializeField] Camera fpscamera;

    [Header("Points")]
    [SerializeField] Transform attackPoint;

    public bool reloading;
    private bool readyToShoot;
    private bool allowInvoke = true;

 
    public void PointerUp()
    {
      weaponButton.PointerUp();
    }

    public void PointerDown()
    {
      weaponButton.PointerDown();
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
        if (weaponManager.shotting)
        {
            if (readyToShoot && !reloading && (bulletsLeft > 0))
            {
                bulletsShots = 0;
                Shoot();
            }
        }
       // Ammo Display.
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText((bulletsLeft/bulletsPerTrap) + " / " + (magazineSize/bulletsPerTrap));
        }

       
        // Automatically Reload.
        if (!reloading && (bulletsLeft <= 0))
        {
            Reload();
        }

        // Reload Button.
        if(weaponManager.isReload)
        {
          if(bulletsLeft<magazineSize)
           {
              Reload();
           }
        }
    }
    
    public void Shoot()
    {
        recoil.Fire();
        readyToShoot = false;
        GunSound();
        AttackPosition();

        // Calculatating direction between target point and attack point.
        directionOfAttackAndHitPoint = targetPoint - attackPoint.position;

        AddingBullets();
        GunEffects();

        // Add force to object
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(hit.normal * impactForce);
        }

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShotting);
            allowInvoke = false;
        }

        ShootMoreBullets();
    }

    // To Shot more than one bullets.
    private void ShootMoreBullets()
    {
        if((bulletsShots<bulletsPerTrap) && (bulletsLeft>0))
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    // Play Gun Sound.
    private void GunSound()
    {
        audioSource.Play();
    }

    // Deciding Attack Position.
    private void AttackPosition()
    {
        // Setting target posotion to middle of screen.
        ray = fpscamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 

        // Deciding the hit point.
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
            ItemsDestroy item = hit.transform.GetComponent<ItemsDestroy>();
            if (item != null)
            {
                item.TakeDamage(damage);
            }
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }
    }

    // Adding Bullets.
    private void AddingBullets()
    {
        // Instantiate bullets.
        currentBullet = Instantiate(bullets, attackPoint.position, Quaternion.identity);

        // Rotate Bullets to Shoot Direction.
        currentBullet.transform.forward = directionOfAttackAndHitPoint.normalized;

        // Add force to bullet.
        currentBullet.GetComponent<Rigidbody>().AddForce(directionOfAttackAndHitPoint.normalized * shootForce, ForceMode.Impulse);

        // Destroy Bullets Automatically. 
        Destroy(currentBullet, 3f);

        // Decreasing Number of Bullets.
        bulletsLeft--;
        bulletsShots++;
    }

    // Adding Gun Effects.
    private void GunEffects()
    {
        if(muzzelFlash != null)
        {
            GameObject currentMuzzelFlash = Instantiate(muzzelFlash,attackPoint.position,Quaternion.identity);
            Destroy(currentMuzzelFlash,5f);
        }

        if (impactEffect != null)
        {
            GameObject currentImpactEffect = Instantiate(impactEffect, targetPoint, Quaternion.LookRotation(directionOfAttackAndHitPoint.normalized));
            Destroy(currentImpactEffect, 5f);
        }
    }
       
    // Reload Start.
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadingFinish", reloadTime);
    }

    // Reload Finish.
    private void ReloadingFinish()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
      
  
}
