using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Death")]
    [SerializeField] private String deathTriggerName = "Die";
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
        
        //Hit flash trigger
        HitFlash flash = GetComponent<HitFlash>();
        if (flash != null)
            flash.Flash();
        
        NotifyHealthChanged();

        if (currentHealth <= 0f)
        {
            Debug.Log("Health reached Zero");
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
    // Prevent double-death
    if (isDead) return;
    isDead = true;

    currentHealth = 0f;
    NotifyHealthChanged();
    OnDied?.Invoke();

    Debug.Log($"{gameObject.name} has died!");

    // Trigger death animation first
    Animator animator = GetComponentInChildren<Animator>();
    if (animator != null)
    {
        Debug.Log("I'm dead");
        animator.SetTrigger(deathTriggerName);
    }
    else

    {
        Debug.LogWarning("No Animator found in Children");
    }

    // Disable collider
    Collider col = GetComponent<Collider>();
    if (col != null)
        col.enabled = false;

    // Disable other behaviours
    MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
    foreach (MonoBehaviour script in scripts)
    {
        if (script != this)
            script.enabled = false;
    }

    enabled = false;

    // Destroy after a delay
    if (destroyOnDeath)
        Destroy(gameObject, destroyDelay);
}

    private void NotifyHealthChanged()
    {
        // Single place that updates all listeners (UI, future systems, etc.).
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}