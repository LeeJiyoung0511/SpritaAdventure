using UnityEngine;

public class Crown : MonoBehaviour, IEquipable, IInteractable
{
    public string GetItemInfo()
    {
        return "클릭으로 장착";
    }

    public void Equip()
    {
        GameManager.Instance.Player.Controller.DashAddSpeed += 5;
        Debug.Log("대쉬 속도 증가");
    }

    public void UnEquip()
    {
        GameManager.Instance.Player.Controller.DashAddSpeed -= 5;
    }
}
