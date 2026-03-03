using UnityEngine;

/// <summary>
/// Debug helper script to check inventory setup and status.
/// Attach to InventoryManager to see real-time debug info.
/// </summary>
public class InventoryDebug : MonoBehaviour
{
    public void LogInventoryStatus()
    {
        InventoryManager manager = InventoryManager.instance;
        
        if (manager == null)
        {
            Debug.LogError("InventoryManager instance not found!");
            return;
        }

        if (manager.slots == null || manager.slots.Length == 0)
        {
            Debug.LogError("No inventory slots assigned!");
            return;
        }

        Debug.Log("=== INVENTORY STATUS ===");
        for (int i = 0; i < manager.slots.Length; i++)
        {
            InventorySlot slot = manager.slots[i];
            
            if (slot == null)
            {
                Debug.LogWarning($"Slot {i}: NULL (not assigned in Inspector!)");
                continue;
            }

            if (slot.IsEmpty())
            {
                Debug.Log($"Slot {i}: EMPTY");
            }
            else
            {
                ItemData item = slot.GetItemData();
                int quantity = slot.GetQuantity();
                
                if (item == null)
                {
                    Debug.LogWarning($"Slot {i}: Item data is NULL!");
                }
                else
                {
                    Debug.Log($"Slot {i}: {item.itemName} × {quantity} (Stackable: {item.isStackable}, Max: {item.GetMaxStackSize()})");
                }
            }
        }
        Debug.Log("=======================");
    }
}

