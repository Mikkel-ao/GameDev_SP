using UnityEngine;
/*
/// <summary>
/// Manages the health of the player or enemies.
/// Communicates with the HealthBar UI to display current health.
/// Handles damage, death, and cleanup.
/// </summary>
public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float health = 100f; // Current health (default: 100)
    private float maxHealth; // Store the maximum health value
    private bool isDead = false;
    [SerializeField] private GameObject deathEffectPrefab;
    private HealthBar healthBar; // Reference to the healthbar UI

    void Start()
    {
        // Store the maximum health value
        maxHealth = health;

        // Try to find the HealthBar component in the scene (only works for the player)
        healthBar = FindFirstObjectByType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            Debug.Log($"Health: Connected to HealthBar. Max health set to {maxHealth}");
        }
    }

    /// <summary>
    /// Reduces health by the given damage amount.
    /// Updates the healthbar UI and triggers death if health <= 0.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! Health: {health}/{maxHealth}");

        // Update the healthbar UI if it exists
        if (healthBar != null)
        {
            healthBar.UpdateHealth(health);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles death: disables animations, spawns effects, disables colliders and scripts.
    /// Destroys the gameobject after 3.4 seconds (death animation duration).
    /// </summary>
    public void Die()
    {
       isDead = true;
       Debug.Log($"{gameObject.name} has died!");
       
       // Trigger death animation
       Animator animator = GetComponent<Animator>();
       if (animator != null)
       {
           animator.SetBool("isDead", true);
       }

       // Spawn death particle effect
       if (deathEffectPrefab != null)
       {
           Instantiate(deathEffectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
       }

       // Disable collider so no more damage can be taken
       Collider col = GetComponent<Collider>();
       if (col != null)
       {
           col.enabled = false;
       }

       // Disable all scripts except animator
       MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
       foreach (MonoBehaviour script in scripts)
       {
           script.enabled = false;
       }

       // Destroy gameobject after death animation finishes
       Destroy(gameObject, 3.4f);
    }
}
*/