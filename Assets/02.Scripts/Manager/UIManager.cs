using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HPBar HPBar;
    public StaminaBar StaminaBar;
    [SerializeField]
    private TextMeshProUGUI m_ItemInfoText;
    [SerializeField]
    private PickUpItemIcon m_PickUpItemIcon;

    private void Awake()
    {
        GameManager.Instance.UIManager = this;
        GameManager.Instance.Player.Interaction.OnCheckItemEvent += UpdateItemInfoText;
        GameManager.Instance.Player.Inventory.OnUpdateInventoryEvent += UpdatePickUpItemIcon;
    }

    private void UpdateItemInfoText(IInteractable item)
    {
        bool isShow = item != null;

        if (isShow)
        {
            m_ItemInfoText.text = item.GetItemInfo();
        }
        else
        {
            m_ItemInfoText.text =string.Empty;
        }
    }

    //획득한 아이템 표시 갱신
    private void UpdatePickUpItemIcon(ItemData item)
    {
        m_PickUpItemIcon.PickUpItemData = item;
    }

    //쿨다운 애니메이션 재생
    public void PlayCoolDownAnimation(float duration)
    {
        m_PickUpItemIcon.PlayPlayCoolDownAnimation(duration);
    }
}
