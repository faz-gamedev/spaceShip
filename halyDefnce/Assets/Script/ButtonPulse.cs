using UnityEngine;

public class ButtonPulse : MonoBehaviour
{
    public float scaleAmount = 1.1f;   
    public float speed = 2f;            

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * (scaleAmount - 1);
        transform.localScale = originalScale * scale;
    }
}
