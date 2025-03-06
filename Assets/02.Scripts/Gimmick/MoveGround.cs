using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float MoveSpeed;
    public float MoveDistance;

    private float m_StartZ;

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
