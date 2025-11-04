using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Meteor : MonoBehaviour
{
    [Header(" damage Seting")]
    public int damage;
    [SerializeField] private GameObject efect;
    [Header(" helth setinge")]
    public float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private float lifeTime;
    [Header(" shake steing")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;





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


        if (other.CompareTag("Player"))
        {
            DestroyMeteor();
        }


    }



    private void DestroyMeteor()
    {


        Destroy(gameObject);

        GameObject a = Instantiate(efect, transform.position, transform.rotation);
        Destroy(a, 2);
    }








}