using System;
using System.Collections;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.Profiling;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class WapenControl : MonoBehaviour
{





    [Header("aime ")]
    public Vector2 minLocalPosition = new Vector2(-2f, -2f);

    public Vector2 maxLocalPosition = new Vector2(2f, 2f);
    public float rayDistance = 1000f;
    [SerializeField] private RectTransform rect;
    [SerializeField] private LayerMask collisionLayers;

    private RaycastHit rayHit;


    [SerializeField] private FixedJoystick joystick;

    public float rayLength = 1000f;
    [SerializeField] private RectTransform uiSpriteTransform;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private Camera worldCamera;
    [SerializeField] private GameObject wapen;

    [Header("wapen ")]

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private String sundWapen;
    [SerializeField] private Transform[] firePoint;
   
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
        false, 
        minValuePool,
        maxValuePool     
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

    private void FixedUpdate()
    {
        Aime();
       
    }
    void Update()
    {



        if (joystick.Horizontal <= -0.2)
        {
            Fire();
        }

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
        UiAnimation();
        if (fireActive)
        {
         
           

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

        if (Physics.Raycast(ray, out hit, rayLength, collisionLayers))
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
       
        for (int i = 0; i < firePoint.Length; i++)
        {
        
            firePoint[i].transform.LookAt(lookTarget);

        }

    }




    public void UiAnimation()
    {




        float t = (joystick.Vertical + 1f) / 2f;
        float f = (joystick.Horizontal + 1f) / 2f;


        float targetY = Mathf.Lerp(minLocalPosition.y, maxLocalPosition.y, t);
        float targetX = Mathf.Lerp(minLocalPosition.x, maxLocalPosition.x, f);


        Vector2 targetPosition = new Vector2(targetX, targetY);


        Vector2 currentPosition = rect.anchoredPosition;




        rect.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, Time.deltaTime * 10f);
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

