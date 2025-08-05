using UnityEngine;

public class LimitAreaPhysics : MonoBehaviour
{
    public Vector3 minBounds = new Vector3(-10f, 0f, -10f);
    public Vector3 maxBounds = new Vector3(10f, 5f, 10f);

    private Rigidbody rb;

    void Start()
    {
        rb =FindAnyObjectByType<SpaceShipController>(). GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 clampedPosition = rb.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minBounds.z, maxBounds.z);

        rb.MovePosition(clampedPosition);
    }
    // نمایش محدوده در Scene View
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = (minBounds + maxBounds) / 2f;
        Vector3 size = maxBounds - minBounds;

        Gizmos.DrawWireCube(center, size);
    }
}
