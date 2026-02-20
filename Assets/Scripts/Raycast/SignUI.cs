using UnityEngine;

public class SignUI : MonoBehaviour
{
    [SerializeField] GameObject welcomePanel;

    void Start()
    {
        welcomePanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            welcomePanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            welcomePanel.SetActive(false);
        }
    }
}
