using UnityEngine;
using UnityEngine.AI;

public class ThornBall : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public float MoveRange = 10f; // 이동 가능한 범위
    public float WaitTime = 3f; // 이동 후 대기 시간
    public float Damage = 10f; // 이동 후 대기 시간

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("MoveToRandomPosition", 0f, WaitTime); // 일정 간격으로 이동
    }

    private void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * MoveRange; // 랜덤 방향
        randomDirection += transform.position; // 현재 위치 기준으로 범위 내 이동
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, MoveRange, NavMesh.AllAreas))
        {
            m_Agent.SetDestination(hit.position); // 이동 가능 지점으로 설정
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out Player player))
            {
                player.Damage(Damage);
            }
        }
    }
}
