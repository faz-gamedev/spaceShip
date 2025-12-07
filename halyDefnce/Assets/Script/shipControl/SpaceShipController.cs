using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    [Header("----Ship movement values-----")]



    [Range(100f, 1000f)]
    [SerializeField]
    private float _thrustForce = 750f;
    [Range(1f, 1000f)]
    [SerializeField]
    private float _pitchForce = 60f;
    [Range(1f, 1000f)]
    [SerializeField]
    private float _rollForce = 20f;
    [Range(1f, 1000f)]
    [SerializeField]
    private float _yawForce = 100;



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

    [Header("------ui anima-------")]
    [SerializeField] private RectTransform rect;
    [SerializeField] private RectTransform rect2;




    private bool deActiveBooster = true;
    private void Awake()
    {


        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 4f;
    }

  [SerializeField]  private float setthrust;
    void Start()
    {
        setthrust = _thrustForce;
        
    }
    private void Update()
    {
        AhromAni();
        if (joystick.Vertical <= 0.2)
        {
            _pitchAmount = joystick.Vertical;
            UiAnimation();
        }
        else if (joystick.Vertical >= 0.2)
        {
            _pitchAmount = joystick.Vertical;
            UiAnimation();
        }

        if (joystick.Horizontal <= 0.2)
        {
            _yawAmount = joystick.Horizontal;
            UiAnimation();
        }
        else if (joystick.Horizontal >= 0.2)
        {
            _yawAmount = joystick.Horizontal;
            UiAnimation();
        }


        if (joystickRoll.Horizontal <= 0.1)
        {

            _rollAmount = -joystickRoll.Horizontal;
        }
        else
        {

            _rollAmount = -joystickRoll.Horizontal;
        }

    }
    private void FixedUpdate()
    {






        if (!Mathf.Approximately(a: 0f, b: _pitchAmount))
        {
            rigidbody.AddTorque(transform.right * (_pitchForce * _pitchAmount));
        }
        if (!Mathf.Approximately(a: 0f, b: _rollAmount))
        {
            rigidbody.AddTorque(transform.forward * (_rollForce * _rollAmount));
        }
        if (!Mathf.Approximately(a: 0f, b: _yawAmount))
        {
            rigidbody.AddTorque(transform.up * (_yawAmount * _yawForce));
        }
        if (!Mathf.Approximately(a: 0f, b: _thrustAmount))
        {
            rigidbody.AddForce(transform.forward * (_thrustForce * _thrustAmount));
        }

    }

    public void BoostSpeed(float _delay,float thrust)
    {
        StartCoroutine(ExecuteAfterTime(_delay, thrust));
    }
    IEnumerator ExecuteAfterTime(float delay,float _thrust)
    {
        deActiveBooster = false;
        _thrustForce = _thrust;
        _thrustAmount = 1;
      
        yield return new WaitForSeconds(delay);

        deActiveBooster = true;
        _thrustForce = setthrust;
    }
    public void UiAnimation()
    {





        float t = (joystick.Vertical + 1f) / 2f;
        float f = (joystick.Horizontal + 1f) / 2f;


        float finalValue = Mathf.Lerp(-183, 183, t);
        float finalValue2 = Mathf.Lerp(-183, 183, f);
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, finalValue);
        rect2.anchoredPosition = new Vector2(rect2.anchoredPosition.x, finalValue2);

    }

    bool ChekEngineUp;

  
    public void SetFors()
    {
        if (deActiveBooster==false)
        {
            return;
        }

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



    public void DeActiveAll()
    {
        _pitchAmount = 0;
        _yawAmount = 0;
        _rollAmount = 0;
        slider.value = 0;
    }
}
