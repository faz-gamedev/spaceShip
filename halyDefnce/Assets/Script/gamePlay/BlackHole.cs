using UnityEngine;

public class BlackHole : MonoBehaviour
{
    // شعاع تأثیر سیاه چاله
    [Tooltip("شعاعی که اجسام در آن شروع به کشیده شدن می‌کنند.")]
    public float pullRadius = 10f;

    // قدرت نیروی کشش
    [Tooltip("قدرت نیروی جاذبه. عدد بالاتر = کشش قوی‌تر.")]
    public float pullForce = 50f;

    // اگر می‌خواهید نیروی کشش هرچه نزدیک‌تر شوید قوی‌تر شود، این را فعال کنید
    [Tooltip("قانون جاذبه معکوس فاصله را اعمال می‌کند (کشش واقعی‌تر).")]
    public bool useDistanceFalloff = true;

    // FixedUpdate برای محاسبات فیزیکی استفاده می‌شود
    void FixedUpdate()
    {
      
       
        Collider[] colliders = Physics.OverlapSphere(transform.position, pullRadius);

       
        foreach (Collider col in colliders)
        {
           
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
              
                if (rb.gameObject == gameObject)
                    continue;

               
                Vector3 blackHolePosition = transform.position;
                Vector3 objectPosition = rb.transform.position;

                
                Vector3 pullDirection = blackHolePosition - objectPosition;

              
                float finalForce = pullForce;

                
                if (useDistanceFalloff)
                {
                  
                    float distance = pullDirection.magnitude;
                  
                    finalForce = (pullForce / distance) * rb.mass;
                }

              
                rb.AddForce(pullDirection.normalized * finalForce * Time.fixedDeltaTime, ForceMode.Force);
            }
        }
    }

   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}