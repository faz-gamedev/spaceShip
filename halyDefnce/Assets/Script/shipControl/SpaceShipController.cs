using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    [Header("----Ship movement values-----")]



    [Range(1000f, 10000f)]
    [SerializeField]
    private float _thrustForce = 7500f;
    [Range(500f, 10000f)]
    [SerializeField]
    private float _pitchForce = 6000f;
    [Range(100f, 10000f)]
    [SerializeField]
    private float _rollForce = 2000f;
    [Range(500f, 10000f)]
    [SerializeField]
    private float _yawForce = 1000;



    [Header("------Ship movement amount-------")]
    [Range(-1, 1)]
    [SerializeField]
    float _thrustAmount = 0;
    [Range(-1, 1)]
    [SerializeField]
    float _pitchAmount = 0;
    [Range(-1, 1)]
    [SerializeField]
    float _rollAmount = 0;
    [Range(-1, 1)]
    [SerializeField]
    float _yawAmount = 0;

    [Header("------input-------")]
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private FixedJoystick joystickRoll;
    [SerializeField] private Slider slider;
    [SerializeField] private bool JiroscopOn = false;
    Rigidbody rigidbody;
    private Gyroscope gyro;
    [Header("------booster-------")]
    [SerializeField] private GameObject[] bostrrEfect;
    [SerializeField] private AudioSource bostrrAudio;
    [Header("-------Audio-------")]
    [SerializeField] private AudioSource boosterAudioSource;
    [SerializeField] private AudioClip startBoosterAudio;
    [Header("------cabin anima-------")]

    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private Transform cabinAhrom;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    void Start()
    {

    }

    private void FixedUpdate()
    {

        AhromAni();
        if (joystick.Vertical <= 0.1)
        {
            _pitchAmount = joystick.Vertical;

        }
        else
        {
            _pitchAmount = joystick.Vertical;
        }

        if (joystick.Horizontal <= 0.1)
        {
            _yawAmount = joystick.Horizontal;

        }
        else
        {
            _yawAmount = joystick.Horizontal;

        }


        if (joystickRoll.Horizontal <= 0.1)
        {

            _rollAmount = -joystickRoll.Horizontal;
        }
        else
        {

            _rollAmount = -joystickRoll.Horizontal;
        }






        if (!Mathf.Approximately(a: 0f, b: _pitchAmount))
        {
            rigidbody.AddTorque(transform.right * (_pitchForce * _pitchAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(a: 0f, b: _rollAmount))
        {
            rigidbody.AddTorque(transform.forward * (_rollForce * _rollAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(a: 0f, b: _yawAmount))
        {
            rigidbody.AddTorque(transform.up * (_yawAmount * _yawForce * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(a: 0f, b: _thrustAmount))
        {
            rigidbody.AddForce(transform.forward * (_thrustForce * _thrustAmount * Time.fixedDeltaTime));
        }
    }

    bool ChekEngineUp;
    public void SetFors()
    {


        if (ChekEngineUp == false && slider.value >= 0)
        {
            boosterAudioSource.PlayOneShot(startBoosterAudio);
        }
        if (slider.value == 0)
        {
           
           
             ChekEngineUp = false;
        }
        else
        {
            ChekEngineUp = true;
        }
        _thrustAmount = slider.value;
        bostrrAudio.volume = slider.value;
        for (int i = 0; i < bostrrEfect.Length; i++)
        {
            Vector3 scale = bostrrEfect[i].transform.localScale;
            scale.y = slider.value + .3f;
            bostrrEfect[i].transform.localScale = scale;

        }
    }

    void AhromAni()
    {

        float horizontalInput = joystick.Horizontal; // چپ و راست
        float verticalInput = joystick.Vertical;    // بالا و پایین

        // محاسبه زاویه‌ها
        float targetAngleZ = horizontalInput * maxRotationAngle; // خم شدن به چپ و راست
        float targetAngleX = verticalInput * maxRotationAngle;     // خم شدن به بالا و پایین



        cabinAhrom.localRotation = Quaternion.Euler(targetAngleX, 0f, -targetAngleZ);
    }
    public void JiroscopActive()
    {
        JiroscopOn = !JiroscopOn;
    }


    public void DeActiveAll()
    {
        _pitchAmount = 0;
        _yawAmount = 0;
        _rollAmount = 0;
        slider.value = 0;
    }
}
