using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.Profiling;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class WapenControl : MonoBehaviour
{


    [Header("sprit move ")]
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private Vector2 minBounds = new Vector2(-300f, -300f); // حداقل موقعیت (x, y)
    [SerializeField] private Vector2 maxBounds = new Vector2(300f, 300f);   // حداکثر موقعیت (x, y)
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private Vector2 initialPosition;


    [Header("aime ")]
    public float rayLength = 1000f;
    [SerializeField] private RectTransform uiSpriteTransform; 
    [SerializeField] private Camera uiCamera; 
    [SerializeField] private Camera worldCamera;   
    [SerializeField] private GameObject wapen;
    [SerializeField] private FixedJoystick joystick;
    [Header("wapen ")]

    [SerializeField] private GameObject bulletPrefab;      
    [SerializeField] private String sundWapen;      
    [SerializeField] private Transform[] firePoint;         
    [SerializeField] private float bulletSpeed = 20f;      
    [SerializeField] private float fireRate = 0.5f;    

    private float nextFireTime = 0f;     // زمان مجاز بعدی برای شلیک
    public bool fireActive = false;

    [Header("Ammo & Cooldown")]


    [SerializeField] private int maxAmmo = 10;
    private int currentAmmo;
    [SerializeField] private UnityEngine.UI.Slider ammoCunt;
    [SerializeField] private float cooldown = 5f;
    private float cooldownTimer = 0f;
    private bool isReloading = false;



    private ObjectPool<GameObject> bulletPool;

    [Header("pool Cunt")]
    [SerializeField] private int minValuePool = 10;
    [SerializeField] private int maxValuePool = 50;
   
    private void Start()
    {
       
        bulletPool = new ObjectPool<GameObject>(
        CreateBullet,
        OnGetBullet,
        OnReleaseBullet,
        OnDestroyBullet,
        false,  // collectionCheck (برای دیباگ مفیده ولی در تولید بهتره false باشه)
        minValuePool,     // تعداد اولیه
        maxValuePool     // حداکثر تعداد
    );
        ammoCunt.maxValue = maxAmmo;
        currentAmmo = maxAmmo;
        ammoCunt.value = currentAmmo;

    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bolet>().SetPool(bulletPool);
        return bullet;
    }

    private void OnGetBullet(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    private void OnReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void OnDestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    void Update()
    {

        ReturnToOrigin();

        if (currentAmmo <= 0)
        {
            if (!isReloading)
            {
                isReloading = true;
                cooldownTimer = 0f;
            }

            cooldownTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(cooldownTimer / cooldown);
            ammoCunt.value = progress * maxAmmo;

            if (cooldownTimer >= cooldown)
            {
                currentAmmo = maxAmmo;
                ammoCunt.value = currentAmmo;
                isReloading = false;
            }
        }
        Aime();
        if (fireActive)
        {
            AimeControl();
           

            if (!isReloading && currentAmmo > 0)
            {
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (direction.magnitude > 0.1f)
        {

        }
        else
        {
            fireActive = false;
        }
    }


    public void Aime()
    {
       
        // موقعیت اسپرایت در صفحه
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, uiSpriteTransform.position);

        // ساخت ری‌کست از موقعیت UI به جلو (از دوربین جهان)
        Ray ray = worldCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        Vector3 lookTarget;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            Vector3 scale = uiSpriteTransform.localScale;
          
            lookTarget = hit.point;
            if (hit.collider.CompareTag("Enemy"))
            {
               
                scale.y = .5f;
                scale.x = .5f;
              
                   uiSpriteTransform.transform.localScale = scale;
            }
    
        }
        else
        {
            Vector3 scale = uiSpriteTransform.localScale;
            scale.y = 1;
            scale.x = 1;
          
              uiSpriteTransform.transform.localScale = scale;

            lookTarget = ray.origin + ray.direction * rayLength;
        }
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        // چرخش آبجکت به سمت نقطه هدف
        for (int i = 0; i < firePoint.Length; i++)
        {
            if (lookTarget==null)
            {
                return;
            }
            firePoint[i].transform.LookAt(lookTarget);

        }
       
    }



    private void ReturnToOrigin()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction.magnitude > 0.1f)
        {

        }
        else
        {

            if (uiSpriteTransform == null) return;


            uiSpriteTransform.anchoredPosition = Vector2.Lerp(
                uiSpriteTransform.anchoredPosition,
                initialPosition,
                Time.deltaTime * returnSpeed
            );
        }

    }
    public void AimeControl()
    {
        if (joystick == null || uiSpriteTransform == null) return;

        // دریافت ورودی جوی‌استیک
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        Vector2 newPos = uiSpriteTransform.anchoredPosition + direction * moveSpeed * Time.deltaTime;

        // محدود کردن موقعیت جدید داخل بازه
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        // اعمال موقعیت نهایی
        uiSpriteTransform.anchoredPosition = newPos; ;

        // موقعیت اسپرایت در فضای صفحه (Screen Space)
        if (uiSpriteTransform == null || uiCamera == null || wapen == null) return;

        // موقعیت اسپرایت در فضای صفحه (Screen Space)
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, uiSpriteTransform.position);
    }
    public void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;
        for (int i = 0; i < firePoint.Length; i++)
        {
           
              GameObject bullet = bulletPool.Get();
            bullet.transform.position = firePoint[i].position;
            bullet.transform.rotation = firePoint[i].rotation;
        }
        currentAmmo--;
        ammoCunt.value = currentAmmo;

    }


    public void Fire()
    {
        fireActive = true;
    }
    public void Firefals()
    {
        fireActive = false;
    }
}

