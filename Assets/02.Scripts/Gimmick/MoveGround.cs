using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float MoveSpeed; //이동속도
    public float MoveDistance; //이동거리

    private float m_StartZ; //시작위치Z

    private void Start()
    {
        m_StartZ = transform.position.z;
    }

    private void Update()
    {
        float newZ = m_StartZ + Mathf.PingPong(Time.time * MoveSpeed, MoveDistance);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
