using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health; 
    private bool isDead = false;
    [SerializeField] private GameObject deathEffectPrefab;
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        health -= damage;
        Debug.Log($"{gameObject.name} hit enemy for {damage}");

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
       isDead = true;
       Debug.Log($"{gameObject.name} has died!");
       
       //Trigger death animation
       Animator animator = GetComponent<Animator>();
       if (animator != null)
       {
           animator.SetBool("isDead",true);
       }

       //Spawn death particle effect
       if (deathEffectPrefab != null)
       {
           Instantiate(deathEffectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
       }
       Collider col = GetComponent<Collider>();
       if (col != null)
       {
           col.enabled = false;
       }
       MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
       foreach (MonoBehaviour script in scripts)
       {
           script.enabled = false;
       }
       Destroy(gameObject,3.4f);
    }
}