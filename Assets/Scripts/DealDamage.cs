using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Fist hit: {other.name}  Tag={other.tag}");

        Health enemy = other.GetComponentInParent<Health>();
        if (enemy != null)
        {
            Debug.Log($"{gameObject.name} dealt {damage} damage!");
            enemy.TakeDamage(damage);
        }
    }
}