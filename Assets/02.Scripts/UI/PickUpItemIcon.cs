using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        if (PickUpItemData != null)
        {
            m_ItemIcon.sprite = PickUpItemData.Sprite;
        }
    }

    //쿨다운 애니메이션 재생
    public void PlayPlayCoolDownAnimation(float duration)
    {
        StartCoroutine(IPlayCoolDownAnimation(duration));
    }

    private IEnumerator IPlayCoolDownAnimation(float duration)
    {
        m_CoolDownImage.fillAmount = 0;
        float time = 0;

        while(time < duration)
        {
            time += Time.deltaTime;
            m_CoolDownImage.fillAmount = Mathf.Lerp(0, 1, time / duration);   //부드럽게 이동
            yield return null;
        }
        m_CoolDownImage.fillAmount = 0;
    }
}
