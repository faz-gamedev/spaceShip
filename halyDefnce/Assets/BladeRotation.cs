using UnityEngine;

public class BladeRotation : MonoBehaviour
{

    public enum Axis
    {
        x,
        y,
        z
    }

    public Axis rotationAxis;
    [SerializeField] private float bladeSpeed;
    public bool inverseRotation = false;

    private float rotateDegree = 0f;

    private void Update()
    {
        float deltaRotation = bladeSpeed * Time.deltaTime;
        if (inverseRotation)
        {
            rotateDegree -= deltaRotation;
        }
        else
        {
            rotateDegree += deltaRotation;
        }

        rotateDegree %= 360;

        Vector3 currentRotation = transform.localEulerAngles;

        switch (rotationAxis)
        {
            case Axis.x:
                transform.localEulerAngles = new Vector3(rotateDegree, currentRotation.y, currentRotation.z);
                break;
            case Axis.y:
                transform.localEulerAngles = new Vector3(currentRotation.x, rotateDegree, currentRotation.z);
                break;
            case Axis.z:
                transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, rotateDegree);
                break;
        }
    }
}
