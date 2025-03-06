using UnityEngine;
using DG.Tweening;

public class JumpPad : MonoBehaviour
{
    public LayerMask JumpPadLayerMask;
    public float JumpPower; //점프힘

    [Header("용수철 애니메이션")]
    public Transform Spring; // 스프링 오브젝트
    public float MaxScaleY; // 늘어나는 최대 스프링 scale값
    public float MinScaleY; // 줄어드는 최소 스프링scale 값
    public float ExpandSpeed; // 늘어나는 속도
    public float CompressSpeed; //줄어드는 속도

    private PlayerController m_PlayerController;

    private void Start()
    {
        m_PlayerController = GameManager.Instance.Player.Controller;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_PlayerController.IsCheckDown(JumpPadLayerMask))
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rigid))
            {
                rigid.AddForce(transform.up * JumpPower, ForceMode.VelocityChange);
                PlaySpringAnimation();
            }
        }
    }

    //용수철 애니메이션
    private void PlaySpringAnimation()
    {
        Spring.DOScaleY(MaxScaleY, ExpandSpeed)
            .SetEase(Ease.OutElastic) // 용수철 반동 효과
            .OnComplete(() =>
            {
                Spring.transform.DOScaleY(MinScaleY, CompressSpeed);
            });
    }
}
