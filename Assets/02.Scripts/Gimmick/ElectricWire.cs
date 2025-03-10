using UnityEngine;

public class ElectricWire : MonoBehaviour
{
    [SerializeField]
    private Transform m_StartPos;  //전깃줄 시작 위치
    [SerializeField]
    private Transform m_EndPos;    //전깃줄 끝 위치
    [SerializeField]
    private LayerMask m_PlayerLayer; //플레이어 레이어

    private void FixedUpdate()
    {
        Check();
    }

    private void Check()
    {
        Vector3 startPos = m_StartPos.position;
        Vector3 endPos = m_EndPos.position;
        Vector3 direction = (endPos - startPos).normalized;
        float distance = Vector3.Distance(startPos, endPos);
        Ray ray = new Ray(startPos, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, m_PlayerLayer))
        {
            if (hit.collider.TryGetComponent(out PlayerController controller))
            {
                controller.Knockback();
            }
        }
    }
}
