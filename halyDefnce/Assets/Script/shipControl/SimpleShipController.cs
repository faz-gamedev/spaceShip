using UnityEngine;
using UnityEngine.UI;

public class SimpleShipController : MonoBehaviour
{
    [Header("سرعت حرکت و چرخش")]
    [SerializeField] private float movementSpeed = 10f; // سرعت جابجایی (متر بر ثانیه)
    [SerializeField] private float rotationSpeed = 150f; // سرعت چرخش (درجه بر ثانیه)

    [Header("ورودی‌ها")]
    // فرض می‌کنیم از همان کامپوننت FloatingJoystick استفاده می‌کنید
    [SerializeField] private FloatingJoystick joystick;

    // ورودی برای رول (چرخش حول محور جلو)
    [SerializeField] private FixedJoystick joystickRoll;

    // ورودی برای رانش (Throttle)
    [SerializeField] private Slider throttleSlider;


    private void Update()
    {
        // 1. دریافت ورودی از جوی استیک‌ها
        float pitchInput = joystick.Vertical;   // بالا و پایین (Pitch)
        float yawInput = joystick.Horizontal;  // چپ و راست (Yaw)
        float rollInput = joystickRoll.Horizontal; // رول (Roll)
        float thrustInput = throttleSlider.value; // رانش (Thrust/سرعت)

        // --- اعمال چرخش (Rotation) ---

        // چرخش Pitch (بالا/پایین) حول محور X محلی
        transform.Rotate(pitchInput * rotationSpeed * Time.deltaTime, 0f, 0f, Space.Self);

        // چرخش Yaw (چپ/راست) حول محور Y محلی
        transform.Rotate(0f, yawInput * rotationSpeed * Time.deltaTime, 0f, Space.Self);

        // چرخش Roll (غلتیدن) حول محور Z محلی
        transform.Rotate(0f, 0f, rollInput * rotationSpeed * Time.deltaTime, Space.Self);


        // --- اعمال حرکت (Movement) ---

        // 2. محاسبه بردار حرکت (به سمت جلو)
        // حرکت همیشه در جهت رو به جلوی سفینه (transform.forward) اعمال می‌شود
        Vector3 thrustDirection = transform.forward * thrustInput * movementSpeed * Time.deltaTime;

        // 3. اعمال حرکت
        transform.position += thrustDirection;
    }
}