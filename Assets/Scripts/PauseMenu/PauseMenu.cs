using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

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
        menuPanel.SetActive(!menuPanel.activeSelf);

        if (menuPanel.activeSelf)
        {
            BindVolumeSlider();
        }
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
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