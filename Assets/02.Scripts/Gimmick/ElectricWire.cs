using UnityEngine;

public class ElectricWire : MonoBehaviour
{
    [SerializeField]
    private Transform m_StartPos;  //전깃줄 시작 위치
    [SerializeField]
    private Transform m_EndPos;    //전깃줄 끝 위치
    [SerializeField]
    private LayerMask m_PlayerLayer; //플레이어 레이어

    public float Damage = 10.0f;

    private void FixedUpdate()
    {
        CheckHitPlayer();
    }

    //전깃줄 레이 충돌 처리
    private void CheckHitPlayer()
    {
        Vector3 startPos = m_StartPos.position;  // 전깃줄 시작위치
        Vector3 endPos = m_EndPos.position; //전깃줄 끝 위치
        Vector3 direction = (endPos - startPos).normalized; //전깃줄 방향
        float distance = Vector3.Distance(startPos, endPos); //시작지점과 끝지점의 전깃줄 거리
        Ray ray = new Ray(startPos, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, m_PlayerLayer))
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                player.Damage(Damage);  // 전깃줄에 닿으면 플레이어 넉백
            }
        }
    }
}
