using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the visual representation of a healthbar.
/// Scales the green fill image based on current health percentage.
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFill; // The green image that represents current health
    private float maxHealth = 100f; // Maximum health (default: 100)
    private float currentHealth; // Current health value

    void Start()
    {
        // Initialize health to max
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    /// <summary>
    /// Called by the Health script when damage is taken.
    /// Updates the healthbar display based on remaining health.
    /// </summary>
    public void UpdateHealth(float newHealth)
    {
        currentHealth = newHealth;
        UpdateHealthBar();
    }

    /// <summary>
    /// Sets the maximum health value (used for initialization).
    /// </summary>
    public void SetMaxHealth(float max)
    {
        maxHealth = max;
        currentHealth = max;
        UpdateHealthBar();
    }

    /// <summary>
    /// Updates the healthbar visual by scaling the fill image.
    /// </summary>
    private void UpdateHealthBar()
    {
        if (healthFill == null)
        {
            Debug.LogError("HealthBar: healthFill Image is not assigned!");
            return;
        }

        // Calculate health percentage (0 to 1)
        float healthPercentage = Mathf.Clamp01(currentHealth / maxHealth);

        // Scale the fill image width based on health percentage
        RectTransform rectTransform = healthFill.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Scale the X axis to represent health percentage
            Vector3 scale = rectTransform.localScale;
            scale.x = healthPercentage;
            rectTransform.localScale = scale;
        }

        Debug.Log($"HealthBar: Health = {currentHealth}/{maxHealth} ({healthPercentage * 100:F1}%)");
    }

    /// <summary>
    /// Returns the current health value (for debugging or UI display).
    /// </summary>
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Returns the max health value.
    /// </summary>
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}

