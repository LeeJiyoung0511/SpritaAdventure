using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HPBar HPBar { get; set; }

    public TextMeshProUGUI ItemInfoText;

    private void Awake()
    {
        GameManager.Instance.UIManager = this;
    }

    private void Start()
    {
        GameManager.Instance.Player.Interaction.OnCheckItemEvent += UpdateItemInfoText;
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
}
