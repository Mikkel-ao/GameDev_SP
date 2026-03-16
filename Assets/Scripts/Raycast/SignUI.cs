using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Handles showing/hiding a UI panel when the player enters/exits a trigger zone.
/// Attach this script to a trigger collider object (e.g., cube, plane).
/// </summary>
public class SignUI : MonoBehaviour
{
    // [FormerlySerializedAs("welcomePanel")] helps unity to search for the old name also 
    [FormerlySerializedAs("welcomePanel")] [SerializeField] private GameObject panelToShow;

    private void Awake()
    {
        // Warn if welcomePanel is not assigned in Inspector
        if (panelToShow == null)
        {
            Debug.LogWarning("SignUI: welcomePanel is not assigned.", this);
            return;
        }

        // Ensure panel starts hidden
        panelToShow.SetActive(false);
    }

    /// <summary>
    /// Called when the player enters the trigger zone.
    /// Shows the welcome panel.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || panelToShow == null)
        {
            return;
        }

        panelToShow.SetActive(true);
    }

    /// <summary>
    /// Called when the player exits the trigger zone.
    /// Hides the welcome panel.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || panelToShow == null)
        {
            return;
        }

        panelToShow.SetActive(false);
    }
}
