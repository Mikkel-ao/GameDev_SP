using UnityEngine;

/// <summary>
/// ScriptableObject that defines an item type.
/// Create assets via: Right-click > Create > Inventory > ItemData
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    public string ItemName = "Unnamed Item";
    [TextArea] public string Description;
    public Sprite Icon;

    [Header("Classification")]
    public ItemCategory Category = ItemCategory.Tools;

    [Header("Stats")]
    public ItemStats Stats = new ItemStats();
}
