using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip soundClip;

    [Header("Effect")]
    [SerializeField] private GameObject Shield;
    [SerializeField] private GameObject explodEff;
    [Header("falde Settings")]

    [SerializeField] private GameObject faledPanel;
    [SerializeField] private GameObject disabelCanves;
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Health Bar UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Optional Events")]
    public UnityEvent onHeal;
    public UnityEvent onDeath;

    public static event Action onTakeDamage;
    private void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;
        GetComponent<ShakeObjact>().Shake(0.3f, 0.4f);
        StartCoroutine(ActivateAndDeactivate(Shield, 2f));
        PlaySound();
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        onTakeDamage?.Invoke();
        UpdateHealthUI();
        healthSlider.value = currentHealth;
        Debug.Log("Player took damage: " + damageAmount + " | Current Health: " + currentHealth);



        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        onHeal?.Invoke();
        UpdateHealthUI();

        Debug.Log("Player healed: " + healAmount + " | Current Health: " + currentHealth);
    }

    private void Die()
    {
        if (CheckpointManager.levelCompletion == false)
        {
            faledPanel.SetActive(true);
            disabelCanves.SetActive(false);
        }

        onDeath?.Invoke();
        gameObject.SetActive(false);
        Instantiate(explodEff, transform.position, transform.rotation);
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;

    private void OnTriggerEnter(Collider other)
    {
        // بازگشت به پول پس از برخورد

        if (other.CompareTag("Enemy"))
        {
            TakeDamage(100);

        }
        else if (other.CompareTag("killEnemy"))
        {
            TakeDamage(20000);
        }
        else if (other.CompareTag("Rocket"))
        {
            TakeDamage(200);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("killEnemy"))
        {
            TakeDamage(20000);
        }
    }

    public void PlaySound()
    {
        if (soundClip == null)
        {
            Debug.LogWarning("❗ No sound clip assigned to PlaySound on " + gameObject.name);
            return;
        }

        audioSource.PlayOneShot(soundClip);
        FindAnyObjectByType<AudioManager>().Play("MiniAlarm");
    }

    private System.Collections.IEnumerator ActivateAndDeactivate(GameObject obj, float duration)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}