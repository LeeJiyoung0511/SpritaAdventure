using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HPBar HPBar;
    public TextMeshProUGUI ItemInfoText;
    [SerializeField]
    private PickUpItemIcon m_PickUpItemIcon;

    private void Start()
    {
        GameManager.Instance.Player.Interaction.OnCheckItemEvent += UpdateItemInfoText;
        GameManager.Instance.Player.Inventory.OnUpdateInventoryEvent += UpdatePickUpItemIcon;
    }

    private void UpdateItemInfoText(IInteractable item)
    {
        bool isShow = item != null;

        ItemInfoText.gameObject.SetActive(isShow);
        if (isShow)
        {
            ItemInfoText.text = item.GetItemInfo();
        }
    }

    //획득한 아이템 표시 갱신
    private void UpdatePickUpItemIcon(ItemData item)
    {
        m_PickUpItemIcon.PickUpItemData = item;
    }
}
