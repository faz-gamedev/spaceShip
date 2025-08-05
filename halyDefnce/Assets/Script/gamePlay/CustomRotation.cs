using UnityEngine;

public class CustomRotation : MonoBehaviour
{
    [Header(" steing")]
    public Vector3 rotationAxis = Vector3.up;     // محور چرخش (پیش‌فرض: Y)
    public float rotationSpeed = 50f;             // سرعت چرخش (درجه بر ثانیه)
    public bool rotateInWorldSpace = false;       // اگر true باشه، چرخش در فضای جهانی انجام میشه

    void Update()
    {
        if (rotateInWorldSpace)
        {
            transform.Rotate(rotationAxis.normalized, rotationSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Rotate(rotationAxis.normalized, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}