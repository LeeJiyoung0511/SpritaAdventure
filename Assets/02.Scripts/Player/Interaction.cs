using System;
using UnityEngine;
using static UnityEditor.Progress;

public class Interaction : MonoBehaviour
{
    public LayerMask m_InteractionLayer;

    private float m_MaxCheckDistance = 5f;
    private float m_CheckRate = 0.2f;
    private float m_LastCheckTime;
    private GameObject m_InteractItem;
    private IInteractable m_CheckItem;

    private readonly Vector3 m_ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

    public Action<IInteractable> OnCheckItemEvent = delegate { }; 

    private void Update()
    {
        if (Time.time - m_LastCheckTime > m_CheckRate)
        {
            m_LastCheckTime = Time.time;
            CheckItem();
        }
    }

    private void CheckItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(m_ScreenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, m_MaxCheckDistance, m_InteractionLayer))
        {
            if (hit.collider.gameObject != m_InteractItem)
            {
                if(hit.collider.TryGetComponent(out IInteractable item))
                {
                    m_InteractItem = hit.collider.gameObject;
                    m_CheckItem = item;
                }
                else
                {
                    m_InteractItem = null;
                    m_CheckItem = null;
                }
            }
        }
        else
        {
            m_InteractItem = null;
            m_CheckItem = null;
        }
        OnCheckItemEvent?.Invoke(m_CheckItem);
    }
}
