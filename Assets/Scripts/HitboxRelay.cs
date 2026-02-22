using UnityEngine;

public class HitboxRelay : MonoBehaviour
{
    public Hitbox rightHandHitbox;

    public void EnableRightHandHitbox()
    {
        Debug.Log("EnableHitbox Event fired");
        rightHandHitbox.EnableHitbox();
    }
    public void DisableRightHandHitbox() => rightHandHitbox.DisableHitbox();
    
    
}

