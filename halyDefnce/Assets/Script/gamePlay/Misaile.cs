using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Misaile : MonoBehaviour
{

    public Transform target;


    [Tooltip("سرعت دنبال کردن موقعیت")]
    public float positionSmoothSpeed = 5f;


    [Tooltip("سرعت نرم تغییر زاویه")]
    public float rotationSmoothSpeed = 5f;

    private Vector3 direction = Vector3.forward;
    public Vector3 offset = new Vector3(0f, 2f, -5f);


    public float delayInSeconds = 3.0f;
    public static event Action OnAttacke;
    private void Start()
    {
        target= GameObject.FindGameObjectWithTag("Player").transform;
        OnAttacke.Invoke();
        Invoke("MethodToCall", delayInSeconds);
    }

    void MethodToCall()
    {
        rotationSmoothSpeed = 9;
        positionSmoothSpeed = 400;
    }
   
    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Smooth Follow: آبجکت هدف (Target) تعیین نشده است!");
            return;
        }



        Vector3 movement = direction * positionSmoothSpeed * Time.deltaTime;

        transform.Translate(movement);


        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, target.up);


        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);


        transform.rotation = smoothedRotation;
    }
}