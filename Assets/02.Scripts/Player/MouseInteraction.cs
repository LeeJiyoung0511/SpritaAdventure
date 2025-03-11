using UnityEngine;

public class MouseInteraction : Interaction
{
    private bool IsMouseInside = false;

    private void Update()
    {
        CheckItem();

        if (Input.GetMouseButton(0) && IsMouseInside)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out IDropable item))
                {
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
