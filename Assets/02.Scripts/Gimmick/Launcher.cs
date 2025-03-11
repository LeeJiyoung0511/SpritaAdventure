using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour, IInteractable
{
    public Transform Transform;

    public CapsuleCollider m_Collider;

    public float RotateSpeed;
    public float MaxEuler;
    public float MinEuler;

    private bool m_IsRiding = false;

    public string GetItemInfo()
    {
        return "닿으면 발사기에 탑승";
    }

    private void Update()
    {
        if (!m_IsRiding) return;

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
            other.transform.position = Transform.position;

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
            m_Collider.enabled = true;
            other.transform.SetParent(null);
        }
    }
}
