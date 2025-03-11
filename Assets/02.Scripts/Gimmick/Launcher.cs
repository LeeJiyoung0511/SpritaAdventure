using UnityEngine;

public class Launcher : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform m_RidePos; // 발사대 탑승 위치

    [Header("발사대 회전")]
    public float RotateSpeed; //회전 속도
    public float MaxEuler; //최대 회전각도
    public float MinEuler; //최소 회전각도

    private bool m_IsRiding = false; //탑승하고 있는지

    public string GetItemInfo()
    {
        return "닿으면 발사기에 탑승";
    }

    private void Update()
    {
        if (!m_IsRiding) return;

        //발사대 회전
        float angle = Mathf.PingPong(Time.time * RotateSpeed, MaxEuler - MinEuler) + MinEuler;
        Quaternion targetRotation = Quaternion.Euler(35, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !m_IsRiding)
        {
            m_IsRiding = true;
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.position = m_RidePos.position;     //플레이어를 탑승위치로 강제 이동

            if (other.gameObject.TryGetComponent(out PlayerController controller))
            {
                controller.RideLancher();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && m_IsRiding)
        {
            m_IsRiding = false;
            other.transform.SetParent(null);
        }
    }
}
