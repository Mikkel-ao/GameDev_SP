using UnityEngine;

public class HitboxController : MonoBehaviour
{
    [SerializeField] private Collider hitbox;

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
}