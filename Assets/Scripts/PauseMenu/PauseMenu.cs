using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private InputSystem_Actions inputActions;

    void Start()
    {
        menuPanel.SetActive(false);

        inputActions = new InputSystem_Actions();
        inputActions.UI.Enable();
        inputActions.UI.Cancel.performed += OnCancelInput;
    }

    void OnDestroy()
    {
        inputActions.UI.Cancel.performed -= OnCancelInput;
        inputActions.Dispose();
    }

    private void OnCancelInput(InputAction.CallbackContext context)
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
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