using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Drives the Inventory UI panel.
///
/// Hierarchy expected:
///  Canvas
///   └─ InventoryPanel          ← assign to inventoryPanel
///       ├─ CategoryTabsRow
///       │   └─ Tab_Tools       ← one Button per category, assign to categoryButtons
///       └─ ItemListContent     ← assign to itemListContent (VerticalLayoutGroup inside a ScrollView)
///           └─ [ItemSlot prefabs spawned at runtime]
///
///  Somewhere on Canvas:
///   └─ InventoryButton         ← the single "Inventory" toggle button; assign to toggleButton
///
///  ItemSlotPrefab              ← assign to itemSlotPrefab
///   ├─ IconImage    (Image)
///   ├─ NameText     (TMP)
///   └─ StatsText    (TMP)
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ItemCollectedBag bag;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Button closeButton;

    [Header("Category Tabs (one per ItemCategory enum value, in order)")]
    [SerializeField] private List<Button> categoryButtons = new List<Button>();

    [Header("Item list")]
    [SerializeField] private Transform itemListContent;
    [SerializeField] private GameObject itemSlotPrefab;

    // ── State ────────────────────────────────────────────────────────────────
    private bool _isOpen = false;
    private ItemCategory _activeCategory = ItemCategory.Tools;

    // ── Lifecycle ────────────────────────────────────────────────────────────
    private void Start()
    {
        // Auto-find the bag on the player if not assigned
        if (bag == null)
            bag = FindFirstObjectByType<ItemCollectedBag>();

        if (bag != null)
            bag.OnInventoryChanged += RefreshItemList;

        // Wire the toggle button
        if (toggleButton != null)
            toggleButton.onClick.AddListener(TogglePanel);

        // Wire the close button
        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePanel);

        // Wire category tab buttons
        ItemCategory[] categories = (ItemCategory[])System.Enum.GetValues(typeof(ItemCategory));
        for (int i = 0; i < categoryButtons.Count && i < categories.Length; i++)
        {
            ItemCategory cat = categories[i]; // capture for lambda
            categoryButtons[i].onClick.AddListener(() => SelectCategory(cat));
        }

        // Start closed
        inventoryPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (bag != null)
            bag.OnInventoryChanged -= RefreshItemList;
    }

    // ── Public API ───────────────────────────────────────────────────────────
    public void TogglePanel()
    {
        _isOpen = !_isOpen;
        inventoryPanel.SetActive(_isOpen);

        if (_isOpen)
            RefreshItemList();
    }

    public void ClosePanel()
    {
        _isOpen = false;
        inventoryPanel.SetActive(false);
    }

    public void SelectCategory(ItemCategory category)
    {
        _activeCategory = category;
        RefreshItemList();
    }

    // ── Private ──────────────────────────────────────────────────────────────
    private void RefreshItemList()
    {
        if (!_isOpen || itemListContent == null || bag == null) return;

        // Clear old slots
        foreach (Transform child in itemListContent)
            Destroy(child.gameObject);

        // Spawn a slot for each item in the selected category
        foreach (ItemData item in bag.GetItemsByCategory(_activeCategory))
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemListContent);
            PopulateSlot(slot, item);
        }
    }

    private void PopulateSlot(GameObject slot, ItemData item)
    {
        // Icon
        Image icon = slot.transform.Find("IconImage")?.GetComponent<Image>();
        if (icon != null && item.Icon != null)
            icon.sprite = item.Icon;

        // Name
        TextMeshProUGUI nameText = slot.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null)
            nameText.text = item.ItemName;

        // Stats
        TextMeshProUGUI statsText = slot.transform.Find("StatsText")?.GetComponent<TextMeshProUGUI>();
        if (statsText != null)
            statsText.text = $"STR {item.Stats.Strength}  INT {item.Stats.Intelligence}  WIL {item.Stats.Willpower}";
    }
}
