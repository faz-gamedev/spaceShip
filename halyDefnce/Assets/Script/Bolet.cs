using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bolet : MonoBehaviour
{
    public int damage = 20;     
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
     
     
    }
    private void OnTriggerEnter(Collider other)
    {
        // بازگشت به پول پس از برخورد

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Meteor>()?.TakeDamage(damage);
        }
        pool.Release(gameObject);
        Debug.Log("takeDamge===-felfopkoefoewooooooooooooooooooooooooooooooooooooooo");
    }


    private void OnTriggerStay(Collider other)
    {
        // بازگشت به پول پس از برخورد

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Meteor>()?.TakeDamage(damage);
        }
        pool.Release(gameObject);
        Debug.Log("takeDamge===-felfopkoefoewooooooooooooooooooooooooooooooooooooooo");
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterSeconds(gameObject, lifeTime));
    }



    IEnumerator DestroyAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        pool.Release(gameObject);
    }
}
