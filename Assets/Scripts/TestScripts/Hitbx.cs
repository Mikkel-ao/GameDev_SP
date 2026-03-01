using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Collider hitboxCollider;

    private void Awake()
    {
        if (hitboxCollider == null)
            hitboxCollider = GetComponent<Collider>();

        hitboxCollider.enabled = false; // off by default
    }

    public void EnableHitbox()
    {
        hitboxCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
    }
}