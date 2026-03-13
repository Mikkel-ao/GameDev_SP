using UnityEngine;
using Unity.Cinemachine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        // Find the Player’s impulse source (on the Player root)
        impulseSource = GetComponentInParent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.transform.root == transform.root) return;

        Health enemy = other.GetComponentInParent<Health>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            // Trigger the screen shake
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
            }
        }
    }
}

/*
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.transform.root == transform.root) return;
        
        Debug.Log($"Punch hit: {other.name}  Tag={other.tag}");

        Health enemy = other.GetComponentInParent<Health>();
        if (enemy != null)
        {
            Debug.Log($"{gameObject.name} dealt {damage} damage!");
            enemy.TakeDamage(damage);
        }
    }
}
*/