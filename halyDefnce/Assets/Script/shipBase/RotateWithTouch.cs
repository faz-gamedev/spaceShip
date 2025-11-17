using UnityEngine;

public class RotateWithTouch : MonoBehaviour
{
    public float rotationSpeed = 5f; 

    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

           
            if (touch.phase == TouchPhase.Moved)
            {
                
                float rotationX = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;

                transform.Rotate(0f, -rotationX, 0f, Space.World);
            }
        }
        else
        {
            transform.Rotate(0f, 10 * Time.deltaTime, 0f, Space.World);
        }
    }
}