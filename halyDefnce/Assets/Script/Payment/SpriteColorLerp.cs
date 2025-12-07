using UnityEngine;
using UnityEngine.UI;

public class SpriteColorLerp : MonoBehaviour
{
    public Image image;
    public Color colorA = Color.white;
    public Color colorB = Color.red;
    public float speed = 2f;

    private float t = 0f;
    private bool forward = true;

    void Update()
    {
        if (image == null) return;

        // تغییر t بین 0 و 1
        if (forward)
        {
            t += Time.deltaTime * speed;
            if (t >= 1f) forward = false;
        }
        else
        {
            t -= Time.deltaTime * speed;
            if (t <= 0f) forward = true;
        }

        // انیمیشن رنگ
        image.color = Color.Lerp(colorA, colorB, t);
    }
}