using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bolet : MonoBehaviour
{
    public int damge = 20;        // سرعت گلوله
    public float speed = 20f;        // سرعت گلوله
    public float lifeTime = 5f;      // مدت زمان عمر گلوله (ثانیه)

    void Start()
    {
 
    }

    void Update()
    {
        // حرکت گلوله به جلو
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private ObjectPool<GameObject> pool;

    public void SetPool(ObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // بازگشت به پول پس از برخورد
        pool.Release(gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterSeconds(gameObject, 1f));
    }



    IEnumerator DestroyAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        pool.Release(gameObject);
    }
}
