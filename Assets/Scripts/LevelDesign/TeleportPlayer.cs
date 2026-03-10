using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float yOffset = 0.1f;
    [SerializeField] private float transitionTime = 0.5f; // seconds

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || destination == null) return;
        StartCoroutine(SmoothTeleport(other.transform));
    }

    private IEnumerator SmoothTeleport(Transform player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        Vector3 startPos = player.position;
        Vector3 endPos = destination.position + new Vector3(0, yOffset, 0);
        float elapsed = 0f;

        while (elapsed < transitionTime)
        {
            player.position = Vector3.Lerp(startPos, endPos, elapsed / transitionTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = endPos;
        player.rotation = destination.rotation;

        if (controller != null) controller.enabled = true;
    }
}