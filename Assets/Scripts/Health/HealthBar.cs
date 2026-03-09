using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the visual representation of a healthbar.
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Health targetHealth;

    private float maxHealth = 100f;
    private float currentHealth = 100f;

    private void OnEnable()
    {
        // Rebind when UI is enabled again (e.g., menu/pause toggles).
        if (targetHealth != null)
        {
            Bind(targetHealth);
        }
        else
        {
            UpdateHealthBar();
        }
    }

    private void OnDisable()
    {
        Unbind();
    }

    public void Bind(Health health)
    {
        // Ensure we are never subscribed to more than one Health source.
        Unbind();

        targetHealth = health;
        if (targetHealth == null)
        {
            return;
        }

        targetHealth.OnHealthChanged += HandleHealthChanged;
        HandleHealthChanged(targetHealth.CurrentHealth, targetHealth.MaxHealth);
    }

    public void Unbind()
    {
        if (targetHealth == null)
        {
            return;
        }

        // Stop listening to avoid duplicate updates/leaks on disable/swap.
        targetHealth.OnHealthChanged -= HandleHealthChanged;
    }

    // Kept for compatibility with any existing calls in your project.
    public void UpdateHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    // Kept for compatibility with any existing calls in your project.
    public void SetMaxHealth(float max)
    {
        maxHealth = Mathf.Max(1f, max);
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void HandleHealthChanged(float newHealth, float newMaxHealth)
    {
        // Keep safe bounds even if values are changed from Inspector/runtime.
        maxHealth = Mathf.Max(1f, newMaxHealth);
        currentHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthFill == null)
        {
            Debug.LogError("HealthBar: healthFill Image is not assigned!");
            return;
        }

        float healthPercentage = Mathf.Clamp01(currentHealth / maxHealth);
        RectTransform rectTransform = healthFill.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // X scale from 0..1 controls the visible fill amount.
            Vector3 scale = rectTransform.localScale;
            scale.x = healthPercentage;
            rectTransform.localScale = scale;
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
