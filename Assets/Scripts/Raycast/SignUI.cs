using UnityEngine;

/// <summary>
/// Handles showing/hiding a UI panel when the player enters/exits a trigger zone.
/// Attach this script to a trigger collider object (e.g., cube, plane).
/// </summary>
public class SignUI : MonoBehaviour
{
    [SerializeField] private GameObject welcomePanel;

    private void Awake()
    {
        // Warn if welcomePanel is not assigned in Inspector
        if (welcomePanel == null)
        {
            Debug.LogWarning("SignUI: welcomePanel is not assigned.", this);
            return;
        }

        // Ensure panel starts hidden
        welcomePanel.SetActive(false);
    }

    /// <summary>
    /// Called when the player enters the trigger zone.
    /// Shows the welcome panel.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || welcomePanel == null)
        {
            return;
        }

        welcomePanel.SetActive(true);
    }

    /// <summary>
    /// Called when the player exits the trigger zone.
    /// Hides the welcome panel.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || welcomePanel == null)
        {
            return;
        }

        welcomePanel.SetActive(false);
    }
}
