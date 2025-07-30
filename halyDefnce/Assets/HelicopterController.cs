using UnityEngine;
using UnityEngine.InputSystem;


public class HelicopterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float tiltAmount = 15f;
    public float tiltSpeed = 5f;
    public float minX = -5f; // حداقل مقدار موقعیت X
    public float maxX = 5f;  // حداکثر مقدار موقعیت X
    public GameObject badane;

    public FloatingJoystick joystick; // از Joystick Pack

    void Update()
    {
        float horizontal = joystick.Horizontal;

        // حرکت چپ و راست در محور X
        Vector3 movement = transform.right * horizontal * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // محدود کردن موقعیت روی خط X
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX); // محدودیت چپ و راست
        transform.position = pos;

        // خم شدن بدنه برای ظاهر طبیعی‌تر
        float targetZRotation = -horizontal * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZRotation);
        badane. transform.rotation = Quaternion.Slerp(badane.transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}


