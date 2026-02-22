using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false; // Start disabled
    }

    public void EnableHitbox()
    {
        _collider.enabled = true;
    }

    public void DisableHitbox()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Example: call enemy damage function
            Debug.Log("Hit enemy!");

            // optional: disable immediately to prevent multi-hits
            // _collider.enabled = false;
        }
    }
}