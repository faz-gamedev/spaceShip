using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Meteor : MonoBehaviour
{
    [Header(" damage Seting")]
    [SerializeField] private int damage;
    [Header(" helth setinge")]
    public float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private float lifeTime;
    [Header(" shake steing")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;

    private ObjectPool<GameObject> pool;


    public void SetPool(ObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }
    void Start()
    {
        currentHealth = maxHealth;
        originalPosition = transform.localPosition;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;


        currentHealth = Mathf.Max(0, currentHealth);

        StartCoroutine(Shake());

        if (currentHealth <= 0)
        {
            DestroyMeteor();
        }
    }


    private System.Collections.IEnumerator Shake()
    {
        float timer = 0f;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = originalPosition + randomOffset;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        // بازگشت به پول پس از برخورد

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
        pool.Release(gameObject);
        Debug.Log("takeDamge===-felfopkoefoewooooooooooooooooooooooooooooooooooooooo");
    }


    private void OnTriggerStay(Collider other)
    {
        // بازگشت به پول پس از برخورد

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        if (pool != null)
        {

            pool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void DestroyMeteor()
    {

        Destroy(gameObject);
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