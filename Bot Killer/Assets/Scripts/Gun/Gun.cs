using TMPro;
using UnityEngine;

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

    [Header("Bullet Prefab")]
    [SerializeField] GameObject bullets;

    [Header("Display")]
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject muzzelFlash;
    [SerializeField] TextMeshProUGUI ammoDisplay;
    [SerializeField] Camera fpscamera;

    [Header("Points")]
    [SerializeField] Transform attackPoint;

    [Header("Animatation")]
    [SerializeField] Animator animator;

    private Vector3 originalRotation;
    private Vector3 originalPosotion;
    [Header("Recoil Posotion")]
    [SerializeField] Vector3 RecoilPosition;
    [SerializeField] Vector3 RecoilRotation;

    private bool readyToShoot, reloading, shotting;
    private bool allowInvoke = true;

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
        originalPosotion = transform.localPosition;
        originalRotation = transform.localEulerAngles;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if (shotting)
        {
            if (readyToShoot && !reloading && (bulletsLeft > 0))
            {
                bulletsShots = 0;
                Shoot();
            }
        }

        UpdatingBulletsMaterial();
    }

    private void UpdatingBulletsMaterial()
    {
            
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
    }

    public void Shoot()
    {
        Recoil();
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
        if (muzzelFlash != null)
        {
            GameObject currentMuzzelFlash = Instantiate(muzzelFlash, attackPoint.position, Quaternion.identity);
            Destroy(currentMuzzelFlash, 5f);
        }

        if (impactEffect != null)
        {
            GameObject currentImpactEffect = Instantiate(impactEffect, targetPoint, Quaternion.LookRotation(directionOfAttackAndHitPoint.normalized));
            Destroy(currentImpactEffect, 5f);
        }
    }
       
    // Reload Start.
    public void Reload()
    {
        animator.SetBool("Reloading", true);
        reloading = true;
        Invoke("ReloadingFinish", reloadTime);
    }

    // Reload Finish.
    private void ReloadingFinish()
    {
        animator.SetBool("Reloading", false);
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void Recoil()
    {
        if ((shotting && !reloading) == true)
        {
            AddRecoil();
        }
        else if ((shotting && !reloading) == false)
        {
            StopRecoil();
        }
    }

    // Add Recoil.
    private void AddRecoil()
    {
        transform.localPosition += RecoilPosition;
        transform.localEulerAngles += RecoilRotation;
    }

    // Stop Recoil.
    private void StopRecoil()
    {
        transform.localPosition = originalPosotion;
        transform.localEulerAngles = originalRotation;
    }
}
