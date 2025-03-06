using UnityEngine;
using UnityEngine.UI;

public class PickUpItemIcon : MonoBehaviour
{
    [SerializeField]
    private Image m_ItemIcon;
    [SerializeField]
    private Image m_CoolDownImage;

    public ItemData PickUpItemData
    {
        get => m_PickUpItemData;
        set
        {
            m_PickUpItemData = value;
            SetIcon();
        }
    }

    private ItemData m_PickUpItemData = null;

    //아이콘 설정
    private void SetIcon()
    {
        m_ItemIcon.gameObject.SetActive(PickUpItemData != null);
        if(PickUpItemData != null)
        {
            m_ItemIcon.sprite = PickUpItemData.Sprite;
        }
    }
}
