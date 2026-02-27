using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the visibility of the inventory UI panel.
/// Listens for TAB key presses and toggles the inventory display.
/// Only works after the player has picked up the bag.
/// </summary>
public class InventoryUIToggle : MonoBehaviour
{
    public GameObject inventoryUI; // drag your Inventory UI panel here
    private bool _hasBag; // Flag: true when player has picked up the bag
    private bool _isOpen; // Flag: tracks if inventory is currently open
    private InputAction _tabAction; // Input action for TAB key detection

    void Start()
    {
        // Hide inventory at start
        inventoryUI.SetActive(false);
        
        // Create Tab input action for the new Input System
        _tabAction = new InputAction("Tab", InputActionType.Button, "<Keyboard>/tab");
        _tabAction.performed += OnTabPressed;
        _tabAction.Enable();
    }

    void OnDestroy()
    {
        // Clean up input action to prevent memory leaks
        if (_tabAction != null)
        {
            _tabAction.performed -= OnTabPressed;
            _tabAction.Disable();
            _tabAction.Dispose();
        }
    }

    /// <summary>
    /// Called when the bag is picked up. Enables this script to listen for TAB input.
    /// </summary>
    public void OnBagPickedUp()
    {
        _hasBag = true;
        
        // Enable this GameObject and its parents so input can be processed
        gameObject.SetActive(true);
        if (gameObject.transform.parent != null)
        {
            gameObject.transform.parent.gameObject.SetActive(true);
        }
        
        Debug.Log("Bag picked up - TAB should now work!");
    }

    /// <summary>
    /// Called when TAB key is pressed. Toggles the inventory panel visibility.
    /// </summary>
    private void OnTabPressed(InputAction.CallbackContext context)
    {
        if (!_hasBag) return; // Only toggle if player has the bag
        
        Debug.Log("TAB PRESSED - Toggling inventory!");
        _isOpen = !_isOpen;
        inventoryUI.SetActive(_isOpen);
    }
}