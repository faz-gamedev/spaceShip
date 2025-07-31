using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class WapenControl : MonoBehaviour
{

    [Header("aime ")]
    [SerializeField] private Camera uiCamera;
    [SerializeField] private RectTransform spriteToMove;    
    [SerializeField] private GameObject wapen;    
    [SerializeField] private FixedJoystick joystick;       
    [Header("wapen ")]

    [SerializeField] private GameObject bulletPrefab;      // پراب گلوله
    [SerializeField] private Transform firePoint;          // نقطه شلیک
    [SerializeField] private float bulletSpeed = 20f;      // سرعت گلوله
    [SerializeField] private float fireRate = 0.5f;        // زمان بین هر شلیک (مثلاً 0.5 یعنی دو گلوله در ثانیه)

    private float nextFireTime = 0f;     // زمان مجاز بعدی برای شلیک
    public bool fireActive = false;

    [Header("Ammo & Cooldown")]


    [SerializeField] private int maxAmmo = 10;
    private int currentAmmo;
    [SerializeField] private Slider ammoCunt;
    [SerializeField] private float cooldown = 5f;
    private float cooldownTimer = 0f;
    private bool isReloading = false;







    private void Start()
    {

        ammoCunt.maxValue = maxAmmo;
        currentAmmo = maxAmmo;
        ammoCunt.value = currentAmmo;

    }
    void Update()
    {

        Aime();

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
    Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, spriteToMove.position);
        // شلیک ری‌کست از موقعیت اسپرایت روی صفحه
        Ray ray = uiCamera.ScreenPointToRay(screenPoint);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);
        Debug.DrawLine(ray.origin, hit.point, Color.green); // برای تست در صحنه
        wapen.transform.LookAt(hit.point);
    }


    public void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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

