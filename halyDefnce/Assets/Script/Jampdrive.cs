using System.Collections;
using UnityEngine;

public class Jampdrive : MonoBehaviour
{
    // --- متغیرهای قابل تنظیم در Inspector ---
    public Transform targetPosition ; // موقعیت نهایی پرش
    public float start = 2.0f; // مدت زمان کل پرش (بر حسب ثانیه)
    public float jumpDuration = 2.0f; // مدت زمان کل پرش (بر حسب ثانیه)
    public float maxStretchScale = 2.5f; // حداکثر ضریب کشیدگی در محور حرکت

    // --- متغیرهای داخلی ---
    private Vector3 initialScale;
    private Vector3 startPosition;
    private bool isJumping = false;

    void Start()
    {
        
        Invoke("StartCor", start);
        // ذخیره مقیاس اصلی شیء
        initialScale = transform.localScale;
        startPosition = transform.position;
    }

    void Update()
    {
     
    }
    public void StartCor()
    {
        startPosition = transform.position;

        StartCoroutine(PerformHyperJump());
    }
    // --- Coroutine اصلی انیمیشن ---
    IEnumerator PerformHyperJump()
    {
        isJumping = true;
        float timeElapsed = 0f;

        // محاسبه جهت حرکت برای اعمال کشیدگی
        Vector3 jumpDirection = (targetPosition.position - startPosition).normalized;

        // در طول مدت زمان پرش حلقه را اجرا کن
        while (timeElapsed < jumpDuration)
        {
            // 1. محاسبه نسبت زمان (0.0 تا 1.0)
            float timeRatio = timeElapsed / jumpDuration;

            // 2. درون یابی خطی (Lerp) برای موقعیت
            // حرکت روان از شروع به پایان
            transform.position = Vector3.Lerp(startPosition, targetPosition.position, timeRatio);

            // 3. محاسبه مقیاس کشیدگی (افکت سینوسی)
            // مقیاس از 1 شروع، به maxStretchScale می رسد، و به 1 بر می گردد
            // از Mathf.Sin(Mathf.PI * timeRatio) استفاده می کنیم که در 0 و 1 مقدار 0، و در 0.5 مقدار 1 را می‌دهد.
            float scaleFactor = (maxStretchScale - 1f) * Mathf.Sin(Mathf.PI * timeRatio) + 1f;

            // 4. اعمال مقیاس

            // محورهای عمود بر جهت حرکت (کمتر فشرده می شوند)
            Vector3 perpendicularScale = initialScale / Mathf.Sqrt(scaleFactor);

            // محور موازی با جهت حرکت (کشیده می شود)
            Vector3 parallelScale = initialScale * scaleFactor;

            // اعمال کشیدگی بر اساس جهت پرش
            // این قسمت تضمین می کند که شیء *فقط* در جهت حرکت کشیده شود و در محورهای دیگر فشرده شود.
            Vector3 currentScale = perpendicularScale;

            // اضافه کردن مؤلفه موازی با جهت پرش
            currentScale += jumpDirection * (parallelScale.magnitude - perpendicularScale.magnitude);

            // تنظیم مقیاس نهایی
            transform.localScale = currentScale;

            // برای فریم بعدی
            timeElapsed += Time.deltaTime;
            yield return null; // صبر تا فریم بعدی
        }

        // --- پایان انیمیشن ---

        // مطمئن شوید که به موقعیت نهایی و مقیاس عادی رسیده است
        transform.position = targetPosition.position;
        transform.localScale = initialScale;

        isJumping = false;

        // اگر می خواهید بلافاصله برای تست برگردد:
        // targetPosition = startPosition; 
        // startPosition = transform.position;
    }
}

