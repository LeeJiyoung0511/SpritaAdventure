using UnityEngine;

public interface IInteractable
{
    public string GetItemInfo();
}

public interface IDropable
{
    public void Drop(Vector3 hitPoint, Vector3 hitNormal);
}

public interface IEquipable
{
    public void Equip();
    public void UnEquip();
}
