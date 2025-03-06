using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemData PickUpItem; //획득한 아이템

    public bool IsCanPickUp => PickUpItem == null; //아이템을 획득할 수 있는지 

    public Action<ItemData> OnUpdateInventoryEvent = delegate { }; // 인벤토리 업데이트 이벤트

    //아이템 획득
    public void PickUp(ItemData pickUpItem)
    {
        PickUpItem = pickUpItem;
        OnUpdateInventoryEvent(pickUpItem);
    }

    //아이템 사용
    public void Use(Player player)
    {
        if (PickUpItem == null) return;
        Debug.Log($"{PickUpItem.ItemName}을 사용했다");
        PickUpItem.ItemEffect.OnEndEffectEvent = ClearItem;
        PickUpItem.ItemEffect.ApplyEffect(player);
    }

    private void ClearItem()
    {
        PickUpItem = null;
        OnUpdateInventoryEvent(null);
    }
}
