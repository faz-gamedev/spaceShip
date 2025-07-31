using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;         // هلی‌کوپتر
   
    [SerializeField] private float smoothSpeed = 5f;   // سرعت دنبال‌کردن
    [SerializeField] private Transform CameraPos;
    [SerializeField] private Transform CameraPos2;
    [SerializeField] private bool sowichCam;
    void FixedUpdate()
    {
        if (target == null) return;

        if (sowichCam==true)
        {
            transform.position = CameraPos2.position;
            transform.rotation = CameraPos2.rotation;
        }
        else
        {
        Vector3 desiredPosition = CameraPos.position;
                
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
            transform.rotation = CameraPos.rotation;

        }
           

    }


   public void SwechCamera()
    {
        sowichCam = !sowichCam;
    }
}
