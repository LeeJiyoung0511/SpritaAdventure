using System;
using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour
{
    public LayerMask m_InteractionLayer; // 감지할 레이어

    private float m_MaxCheckDistance = 5f; // 레이의 최대 감지 거리
    private GameObject m_CheckObject; // 감지한 물체
    private IInteractable m_CheckItem; // 감지한 아이템
    public Action<IInteractable> OnCheckItemEvent = delegate { }; // 아이템 감지 이벤트

    private GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = GameManager.Instance;
        StartCoroutine(ICheckItem());
    }

    private IEnumerator ICheckItem()
    {
        while (true)
        {
            CheckItem();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CheckItem()
    {
        //레이를 쏠 위치 설정
        Vector3 rayPos = transform.position + Vector3.up * 0.35f; 
        //레이의 감지 범위 설정
        float sphereRadius = 0.3f;
        Ray ray = new Ray(rayPos, transform.forward);
        //레이 표시
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * m_MaxCheckDistance, Color.red);
        //레이 충돌
        //SphereCast를 이용해 레이 폭을 늘리기
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, m_MaxCheckDistance, m_InteractionLayer))
        {
            if (hit.collider.gameObject != m_CheckObject)
            {
                if (hit.collider.TryGetComponent(out IInteractable item))
                {
                    m_CheckObject = hit.collider.gameObject;
                    m_CheckItem = item;
                }
                else
                {
                    NotCheckedItem();
                }
            }
        }
        else
        {
            NotCheckedItem();
        }
        OnCheckItemEvent?.Invoke(m_CheckItem);
    }

    //감지가 안되면
    private void NotCheckedItem()
    {
        m_CheckObject = null;
        m_CheckItem = null;
    } 
}
