using UnityEngine;

public class Bolet : MonoBehaviour
{
    public int damge = 20;        // سرعت گلوله
    public float speed = 20f;        // سرعت گلوله
    public float lifeTime = 5f;      // مدت زمان عمر گلوله (ثانیه)

    void Start()
    {
        // حذف خودکار گلوله بعد از lifeTime
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // حرکت گلوله به جلو
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }




}
