using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private InventoryUIToggle inventoryUIToggle; // Reference to inventory UI for disabling input

    private InputSystem_Actions inputActions;
    
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        var soundManager = FindFirstObjectByType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.BindSlider(volumeSlider);
        }
    }
    

    void Start()
    {
        menuPanel.SetActive(false);

        inputActions = new InputSystem_Actions();
        inputActions.UI.Enable();
        inputActions.UI.Cancel.performed += OnCancelInput;

        // Auto-find InventoryUIToggle if not assigned
        if (inventoryUIToggle == null)
        {
            inventoryUIToggle = FindFirstObjectByType<InventoryUIToggle>();
            
            if (inventoryUIToggle != null)
            {
                Debug.Log("PauseMenu: Found InventoryUIToggle automatically!");
            }
            else
            {
                Debug.LogWarning("PauseMenu: Could not find InventoryUIToggle! Inventory won't close when pausing.");
            }
        }
    }

    void OnDestroy()
    {
        if (inputActions != null)
        {
            inputActions.UI.Cancel.performed -= OnCancelInput;
            inputActions.Dispose();
        }
    }

    private void OnCancelInput(InputAction.CallbackContext context)
    {
        bool wasPaused = menuPanel.activeSelf;
        menuPanel.SetActive(!menuPanel.activeSelf);
        bool isPaused = menuPanel.activeSelf;

        Debug.Log($"PauseMenu: Pause menu is now {(isPaused ? "OPEN" : "CLOSED")}");

        // Disable inventory input when pause menu opens
        if (inventoryUIToggle != null)
        {
            inventoryUIToggle.SetInputEnabled(!isPaused);
            Debug.Log($"PauseMenu: Set inventory input enabled = {!isPaused}");
        }
        else
        {
            Debug.LogWarning("PauseMenu: inventoryUIToggle is NULL! Can't communicate with inventory!");
        }

        if (menuPanel.activeSelf)
        {
            BindVolumeSlider();
        }
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        
        // Re-enable inventory input when pause menu closes
        if (inventoryUIToggle != null)
        {
            inventoryUIToggle.SetInputEnabled(true);
        }
    }

    private void BindVolumeSlider()
    {
        var soundManager = FindFirstObjectByType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.BindSlider(volumeSlider);
        }
    }

    public void Quit()
    {
        Application.Quit();
        // Stopper spillet i editor også
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}