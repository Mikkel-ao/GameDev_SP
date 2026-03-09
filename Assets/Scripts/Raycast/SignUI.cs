using UnityEngine;

public class SignUI : MonoBehaviour
{
    [SerializeField] private GameObject welcomePanel;

    private void Awake()
    {
        if (welcomePanel == null)
        {
            Debug.LogWarning("SignUI: welcomePanel is not assigned.", this);
            return;
        }

        welcomePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || welcomePanel == null)
        {
            return;
        }

        welcomePanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || welcomePanel == null)
        {
            return;
        }

        welcomePanel.SetActive(false);
    }
}
