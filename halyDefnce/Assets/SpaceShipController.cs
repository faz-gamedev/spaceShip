using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 30f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 5f;

    [Header("Boost")]
    public float boostForce = 100f;

    [Header("Joystick")]
    public FloatingJoystick joystick;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 inputDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (inputDirection.magnitude > 0.1f)
        {
            // اعمال نیرو به سمت جویستیک
            rb.AddForce(inputDirection.normalized * moveForce);

            // محدود کردن سرعت
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }

            // چرخش به سمت جهت حرکت
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    /// <summary>
    /// فراخوانی برای افزایش ناگهانی سرعت (Boost)
    /// </summary>
    public void Boost()
    {
        Vector3 boostDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (boostDirection == Vector3.zero)
        {
            boostDirection = transform.forward; // اگر حرکتی نیست، مستقیم جلو
        }

        rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
    }
}