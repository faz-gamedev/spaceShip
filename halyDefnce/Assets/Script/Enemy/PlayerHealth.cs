using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Health Bar UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Optional Events")]
    public UnityEvent onTakeDamage;
    public UnityEvent onHeal;
    public UnityEvent onDeath;

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
        Debug.Log("Player died.");
        onDeath?.Invoke();
        // مثلا: کنترل غیر فعال، انیمیشن، پایان بازی و غیره...
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
}