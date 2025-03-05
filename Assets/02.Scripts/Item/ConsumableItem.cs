using UnityEngine;


public class ConsumableItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemData m_ItemData;


    public string GetItemInfo()
    {
        return $"{m_ItemData.ItemName}\n{m_ItemData.ItemDesc}";
    }
}
