using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class WapenControl : MonoBehaviour
{

    [Header("aime ")]



    [SerializeField] private RectTransform spriteToMove;      // اسپرایت داخل UI (مثلاً یک Image)
    [SerializeField] private FixedJoystick joystick;        // جوی‌استیک
    [SerializeField] private float moveSpeed = 200f;           // سرعت حرکت (قابل تنظیم)
    [SerializeField] private Camera uiCamera;                  // دوربینی که UI رو نمایش می‌ده (اغلب main camera)
    [SerializeField] private LayerMask groundLayer;            // لایه‌ای که زمین در آن قرار دارد
    [SerializeField] private GameObject wapen;

    public Vector2 minBounds = new Vector2(-300f, -300f); // حداقل موقعیت (x, y)
    public Vector2 maxBounds = new Vector2(300f, 300f);   // حداکثر موقعیت (x, y)
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float returnSpeed = 5f;
   [SerializeField] private Vector2 initialPosition;
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

    [Header("Active & deActive Trigger")]

    [SerializeField] private FixedJoystick joystickWapen;
    [SerializeField] private FixedJoystick joystickLuncher;
    [SerializeField] private EventTrigger triggerWapen;
    [SerializeField] private EventTrigger triggerLuncher;
   
    
   


   
    private void Start()
    {
      
        ammoCunt.maxValue = maxAmmo;
        currentAmmo = maxAmmo;
        ammoCunt.value = currentAmmo;
      
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

        if (fireActive)
        {
            Aime();

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
    public void DeActiveButton(bool taggle)
    {
        joystickWapen.enabled = taggle;
        joystickLuncher.enabled = taggle;
        triggerWapen.enabled = taggle;
        triggerLuncher.enabled = taggle;
    }

    private void ReturnToOrigin()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction.magnitude > 0.1f)
        {
          
        }
        else
        {
           
        if (spriteToMove == null) return;

       
        spriteToMove.anchoredPosition = Vector2.Lerp(
            spriteToMove.anchoredPosition,
            initialPosition,
            Time.deltaTime * returnSpeed
        );
        }

    }
    public void Aime()
    {
        if (joystick == null || spriteToMove == null) return;

        // دریافت ورودی جوی‌استیک
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        Vector2 newPos = spriteToMove.anchoredPosition + direction * moveSpeed * Time.deltaTime;

        // محدود کردن موقعیت جدید داخل بازه
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        // اعمال موقعیت نهایی
        spriteToMove.anchoredPosition = newPos; ;

        // موقعیت اسپرایت در فضای صفحه (Screen Space)
        if (spriteToMove == null || uiCamera == null || wapen == null) return;

        // موقعیت اسپرایت در فضای صفحه (Screen Space)
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, spriteToMove.position);

        // شلیک ری‌کست از موقعیت اسپرایت روی صفحه
        Ray ray = uiCamera.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green); // برای تست در صحنه
            wapen.transform.LookAt(hit.point);
        }
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

