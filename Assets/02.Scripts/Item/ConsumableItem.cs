using UnityEngine;

public class ConsumableItem : MonoBehaviour, IInteractable
{
    public ItemData ItemData;
    private Inventory m_Inventory;

    private void Start()
    {
        m_Inventory = GameManager.Instance.Player.Inventory;
    }

    public string GetItemInfo()
    {
        return $"{ItemData.ItemName}\n{ItemData.ItemDesc}";
    }

    //아이템 획득
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_Inventory.IsCanPickUp)
        {
            //아이템추가
            m_Inventory.PickUp(ItemData);
            //아이템삭제
            Destroy(gameObject);
            Debug.Log($"{ItemData.ItemName}을 획득했다!");
        }
    }
}
