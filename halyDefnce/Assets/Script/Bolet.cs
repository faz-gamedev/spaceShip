using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bolet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float speed = 20f;        // سرعت گلوله
    [SerializeField] private float lifeTime = 5f;      // مدت زمان عمر گلوله (ثانیه)
    [SerializeField] private GameObject hitEfx;

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
        if (other.CompareTag("Enemy"))
        {

            Vector3 closestPoint = other.ClosestPoint(transform.position);

            GameObject eff = Instantiate(hitEfx, closestPoint, Quaternion.identity);
            Destroy(eff, 3f);
            other.GetComponent<Meteor>()?.TakeDamage(damage);
            pool.Release(gameObject);
        }

    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Meteor>()?.TakeDamage(damage);
        }
        pool.Release(gameObject);

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
