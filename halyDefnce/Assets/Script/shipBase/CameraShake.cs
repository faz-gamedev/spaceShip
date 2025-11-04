using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance { get; private set; }

    [Header("Follow Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private GameObject cabin;
    [SerializeField] private Transform camPositionDefault;
    [SerializeField] private Transform camPositionAlternative;
    [SerializeField] private float positionSmoothTime = 9f;
    [SerializeField] private bool switchCam = false;

    [Header("Shake Settings")]
    [SerializeField] private float rotationMultiplier = 15f;

    private Transform cam;
    private Vector3 shakeInitialLocalPos;
    private Quaternion shakeInitialLocalRot;

    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private float shakeRotation;
    private bool isShaking;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        cam = Camera.main.transform;
    }

    private void Start()
    {
        shakeInitialLocalPos = cam.localPosition;
        shakeInitialLocalRot = cam.localRotation;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        if (!switchCam)
        {
            Vector3 desiredPosition = camPositionDefault.position;
            Vector3 smoothedPosition = Vector3.Lerp(cam.position, desiredPosition, positionSmoothTime * Time.deltaTime);
            cam.position = smoothedPosition;
            cam.rotation = camPositionDefault.rotation;
        }
        else
        {
            cam.rotation = camPositionAlternative.rotation;
        }
    }

    private void Update()
    {
        if (switchCam)
        {
            cam.position = camPositionAlternative.position;
        }

        HandleShake();
    }

    // 🔄 سوییچ بین دوربین‌ها (کابین / بیرون)
    public void SwitchCamera()
    {
        switchCam = !switchCam;
        cabin.SetActive(switchCam);
    }

    // 🎥 متد فراخوانی لرزش از هرجای بازی
    public void Shake(float duration, float magnitude)
    {
        shakeTimeRemaining = duration;
        shakePower = magnitude;
        shakeFadeTime = magnitude / duration;
        isShaking = true;
    }

    private void HandleShake()
    {
        if (!isShaking) return;

        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float x = Random.Range(-1f, 1f) * shakePower;
            float y = Random.Range(-1f, 1f) * shakePower;

            cam.localPosition = shakeInitialLocalPos + new Vector3(x, y, 0);

            shakeRotation = Mathf.Lerp(shakeRotation, Random.Range(-1f, 1f) * shakePower * rotationMultiplier, Time.deltaTime * 10f);
            cam.localRotation = Quaternion.Euler(0, 0, shakeRotation);

            shakePower = Mathf.MoveTowards(shakePower, 0, shakeFadeTime * Time.deltaTime);
        }
        else
        {
            isShaking = false;
            cam.localPosition = Vector3.Lerp(cam.localPosition, shakeInitialLocalPos, Time.deltaTime * 10f);
            cam.localRotation = Quaternion.Lerp(cam.localRotation, shakeInitialLocalRot, Time.deltaTime * 10f);
        }
    }
}