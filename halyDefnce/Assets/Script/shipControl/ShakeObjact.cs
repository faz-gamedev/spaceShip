using UnityEngine;

public class ShakeObjact : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float rotationMultiplier = 10f;

    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private bool isShaking;

    private Vector3 shakeOffset;
    private Quaternion shakeRotation;

    private void Update()
    {
        if (isShaking)
        {
            if (shakeTimeRemaining > 0)
            {
                shakeTimeRemaining -= Time.deltaTime;

                // تولید افست تصادفی اطراف موقعیت فعلی
                float x = Random.Range(-1f, 1f) * shakePower;
                float y = Random.Range(-1f, 1f) * shakePower;
                float z = Random.Range(-1f, 1f) * shakePower * 0.5f;

                shakeOffset = new Vector3(x, y, z);

                float rotZ = Random.Range(-1f, 1f) * shakePower * rotationMultiplier;
                shakeRotation = Quaternion.Euler(0, 0, rotZ);

                shakePower = Mathf.MoveTowards(shakePower, 0, shakeFadeTime * Time.deltaTime);
            }
            else
            {
                isShaking = false;
                shakeOffset = Vector3.zero;
                shakeRotation = Quaternion.identity;
            }
        }

        // اعمال لرزش روی موقعیت فعلی (بدون تداخل با حرکت)
        transform.localPosition += shakeOffset * Time.deltaTime;
        transform.localRotation *= Quaternion.Slerp(Quaternion.identity, shakeRotation, Time.deltaTime * 10f);
    }

    /// <summary>
    /// لرزش موقت هنگام حرکت
    /// </summary>
    public void Shake(float duration, float magnitude)
    {
        shakeTimeRemaining = duration;
        shakePower = magnitude;
        shakeFadeTime = magnitude / duration;
        isShaking = true;
    }
}