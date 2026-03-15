using UnityEngine;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    private Renderer[] renderers;
    private Color[] originalColors;

    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.1f;

    void Awake()
    {
        // Get all renderers in children
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
        Debug.Log("Flash triggered");
    }

    private IEnumerator FlashRoutine()
    {
        // Set all materials to flash color
        foreach (var r in renderers)
            r.material.color = flashColor;

        yield return new WaitForSeconds(flashTime);

        // Restore original colors
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }
}