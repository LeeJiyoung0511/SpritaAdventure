using UnityEngine;

public class MouseInteraction : Interaction
{
    private bool IsMouseInside = false;

    private void Update()
    {
        CheckItem();

        //오브젝트안에 마우스가 있고 마우스 왼쪽 버튼 클릭하면
        if (Input.GetMouseButton(0) && IsMouseInside)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out IDropable item))
                {
                    // 아이템 드롭
                    item.Drop(hit.point, hit.normal);
                }
            }
        }
    }

    protected override void CheckItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out IInteractable item))
            {
                m_CheckObject = hit.collider.gameObject;
                m_CheckItem = item;
                IsMouseInside = true;
            }
            else
            {
                NotCheckedItem();
            }
        }
        else
        {
            NotCheckedItem();
            IsMouseInside = false;
        }
        OnCheckItemEvent?.Invoke(m_CheckItem);
    }
}
