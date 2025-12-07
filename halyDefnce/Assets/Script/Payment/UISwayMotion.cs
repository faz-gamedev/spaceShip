using UnityEngine;

public class UISwayMotion : MonoBehaviour
{
    // === متغیرهای قابل تنظیم در Inspector ===

    [Header("Motion Parameters")]
    // میزان جابجایی از مرکز (به واحد پیکسل)
    public float movementRangeX = 50f;

    // سرعت حرکت نوسانی (فرکانس)
    public float swaySpeed = 2f;

    // === متغیرهای داخلی ===
    private RectTransform rectTransform;
    private float originalX;

    void Start()
    {
        // اطمینان از اینکه GameObject دارای RectTransform است (که برای UI ضروری است)
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("The GameObject does not have a RectTransform component.");
            enabled = false; // اسکریپت را غیرفعال می‌کند
            return;
        }

        // ذخیره موقعیت X اولیه یا مرکزی
        originalX = rectTransform.anchoredPosition.x;
    }

    void Update()
    {
        // 1. محاسبه مقدار نوسان (Sway Value)

        // تابع Mathf.Sin(Time.time * speed) یک مقدار موجی بین -1 تا +1 تولید می‌کند.
        float sinValue = Mathf.Sin(Time.time * swaySpeed);

        // 2. محاسبه موقعیت X جدید

        // مقدار موجی (-1 تا +1) را در movementRangeX ضرب می‌کنیم تا جابجایی 
        // در بازه [-movementRangeX, +movementRangeX] قرار گیرد.
        float newXPosition = originalX + (sinValue * movementRangeX);

        // 3. اعمال موقعیت جدید

        // فقط مقدار X را تغییر می‌دهیم و Y و Z را ثابت نگه می‌داریم.
        Vector2 newPosition = rectTransform.anchoredPosition;
        newPosition.x = newXPosition;

        rectTransform.anchoredPosition = newPosition;
    }
}