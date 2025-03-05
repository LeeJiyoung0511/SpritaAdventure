using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public string ItemDesc;
    public ItemType ItemType;
    public GameObject ItemPrefab;
}

public enum ItemType
{
    Equip,
    Consumable,
}
