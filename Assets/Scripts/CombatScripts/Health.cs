using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Death")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private float destroyDelay = 3.4f;

    [Header("UI (Optional)")]
    [SerializeField] private HealthBar healthBar;

    private bool isDead;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => isDead;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDied;

    private void Awake()
    {
        if (maxHealth <= 0f)
        {
            maxHealth = 1f;
        }

        // If current health was not set in Inspector, start at full health.
        if (currentHealth <= 0f)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }
    }

    private void Start()
    {
        // Optional direct link so this Health can drive a UI bar (typically player).
        if (healthBar != null)
        {
            healthBar.Bind(this);
        }

        // Push initial value so UI starts in sync.
        NotifyHealthChanged();
    }

    public void TakeDamage(float damage)
    {
        // Ignore invalid hits and extra hits after death.
        if (isDead || damage <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Max(0f, currentHealth - damage);
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}/{maxHealth}");

        NotifyHealthChanged();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead || amount <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        NotifyHealthChanged();
    }

    public void Die()
    {
        // Prevent double-death side effects (events, FX, destroy calls).
        if (isDead)
        {
            return;
        }

        isDead = true;
        currentHealth = 0f;
        NotifyHealthChanged();
        OnDied?.Invoke();

        Debug.Log($"{gameObject.name} has died!");

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Disable other behaviours so dead objects stop acting immediately.
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }

        enabled = false;

        if (destroyOnDeath)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private void NotifyHealthChanged()
    {
        // Single place that updates all listeners (UI, future systems, etc.).
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}