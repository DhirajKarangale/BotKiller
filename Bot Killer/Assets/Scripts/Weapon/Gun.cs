using UnityEngine;
using TMPro;


public class Gun : MonoBehaviour
{
    // Object in Script.
    private AudioSource audioSource;
    private GameObject currentBullet;
    private GameObject currentBulletHole;
    private Vector3 directionOfAttackAndHitPoint;
    private Vector3 targetPoint;
    private RaycastHit hit;
    private Ray ray;

    [Header("Bullets Attributes")]
    [SerializeField] byte shootForce;
    [SerializeField] int impactForce;
    [SerializeField] byte damage;
    [SerializeField] float timeBetweenShotting, reloadTime, timeBetweenShots;
    [SerializeField] byte magazineSize, bulletsPerTrap;
    private byte bulletsLeft, bulletsShots;
   

    [Header("Prefab")]
    [SerializeField] GameObject bullets;
    [SerializeField] GameObject bulletHole;
    [SerializeField] RecoilPushBack recoil;
    [SerializeField] WeaponButton weaponButton;
    [SerializeField] Animatation animatation;
    [SerializeField] Animator animator;
     
    [Header("Display")]
    [SerializeField] GameObject muzzelFlash;
    [SerializeField] GameObject impactEffect;
    public TextMeshProUGUI ammoDisplay;
    [SerializeField] Camera fpscamera;

    [Header("Scope")]
    [SerializeField] GameObject scopeOverLay;
    [SerializeField] GameObject weaponCamera;
    [SerializeField] float scopeFOV;
    private float originalFov;

    [Header("Points")]
    [SerializeField] Transform attackPoint;
    
    private bool readyToShoot;
    private bool allowInvoke = true;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;     
    }

    private void Start()
    { 
        audioSource = GetComponent<AudioSource>();
        originalFov = fpscamera.fieldOfView;
    }

    private void Update() 
    {  
        if (weaponButton.shotting && readyToShoot && !animatation.reloading && (bulletsLeft > 0))
        { 
            bulletsShots = 0;
            Shoot();
        }
 
        if(weaponButton.isScope && readyToShoot && !animatation.reloading && (bulletsLeft > 0))
        {
          Invoke("ScopeOn",0.15f);
        }
        if(!weaponButton.isScope) ScopeOff();
              

       // Ammo Display.
       if(ammoDisplay != null)
       {
          ammoDisplay.SetText((bulletsLeft/bulletsPerTrap) + " / " + (magazineSize/bulletsPerTrap));
       }

          
        // Automatically Reload.
        if (!animatation.reloading && (bulletsLeft <= 0))
        {
            Reload();
        }

        // Reload Button.
        if(weaponButton.isReload && (bulletsLeft<magazineSize))
        {
             Reload();
        }
        weaponButton.isReload =false;
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
            hit.rigidbody.AddForce(-hit.normal * impactForce);
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

            //Declearing Item Destroy
            ItemsDestroy item = hit.transform.GetComponent<ItemsDestroy>();
            // Destroying Item
            if(item != null)
            {
                item.TakeDamage(damage);
            }

            //Declearing Enemy to Destroy.
            Health_Death enemy = hit.transform.GetComponent<Health_Death>();
            // Destroy Enemy.
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Bullet Hole
            if(hit.collider.tag == "Sorrounding")
            {
             if(bulletHole != null)
             {
                  // Instantiate bullet Hole.                                                      
             currentBulletHole = Instantiate(bulletHole,targetPoint + hit.normal * 0.001f,Quaternion.identity);
             
             // Rotating Bullet hole to direction of target.
             currentBulletHole.transform.LookAt(hit.point + hit.normal);

              // Destroy Bullet Hole.
             Destroy(currentBulletHole,5f);
             }
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
            Destroy(currentMuzzelFlash,2f);
        }

        if (impactEffect != null)
        {
            GameObject currentImpactEffect = Instantiate(impactEffect, targetPoint, Quaternion.LookRotation(directionOfAttackAndHitPoint.normalized));
            Destroy(currentImpactEffect, 3f);
        }
    }
       
    // Reload Start.
    private void Reload()
    {
        animatation.reloading = true;
        Invoke("ReloadingFinish", reloadTime);
    }

    // Reload Finish.
    private void ReloadingFinish()
    {
        bulletsLeft = magazineSize;
        animatation.reloading = false;
    }

    private void ScopeOn()
    {
        scopeOverLay.SetActive(true);
        weaponCamera.SetActive(false);
        fpscamera.fieldOfView = scopeFOV;
    }

    private void ScopeOff()
    {
        scopeOverLay.SetActive(false);
        weaponCamera.SetActive(true);
        fpscamera.fieldOfView = originalFov;
    }
}
