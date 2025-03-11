using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("이동")]
    public float MoveSpeed;      //이동 속도
    public float DashAddSpeed = 10.0f; //대쉬중일때 더할 속도값
    private Vector2 m_MovementInput;   //입력받은 이동값
    private bool IsMove = false; // 이동중인지
    public bool IsDash = false; // 대쉬중인지

    [Header("점프")]
    public float JumpPower = 100f; //점프
    [SerializeField]
    private LayerMask m_GroundMask;
    [SerializeField]
    private LayerMask m_WallMask;
    public bool IsWallMove = false;

    [Header("시선")]
    [SerializeField]
    private Transform m_CameraContainer;
    private float m_MinXLook = -3f;  // 최소 시야각
    private float m_MaxXLook = 3.0f;  // 최대 시야각
    private float m_CamCurXRot; //현재카메라
    private float m_lookSensitivity = 0.1f; // 카메라 민감도
    private Vector2 mouseDelta;  // 마우스 변화값

    private bool m_IsRiding;

    private Rigidbody m_Rigidbody;
    private Animator m_Animator;

    private Vector3 m_StartPos;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();

        //처음 위치 저장
        m_StartPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (m_IsRiding) return;

        Move();
        WallMove();

        //낙하시 처음 위치로 리스폰
        if (m_Rigidbody.velocity.y < -20.0f)
        {
            m_Rigidbody.velocity = Vector3.zero;
            transform.position = m_StartPos;
        }
    }

    private void LateUpdate()
    {
        if (IsWallMove || m_IsRiding) return;
        CameraLook();
    }

    //플레이어 이동
    private void Move()
    {
        Vector3 dir = (transform.forward * m_MovementInput.y + transform.right * m_MovementInput.x).normalized;
        dir *= MoveSpeed;
        dir.y = m_Rigidbody.velocity.y;
        m_Rigidbody.velocity = dir;
    }

    //플레이어 이동 입력
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            m_MovementInput = context.ReadValue<Vector2>();
            IsMove = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            m_MovementInput = Vector2.zero;
            IsMove = false;
        }
    }

    //점프
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (IsCheckDown(m_GroundMask))
            {
                m_Rigidbody.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
            }
            else if (m_IsRiding)
            {
                m_Rigidbody.useGravity = true;
                m_Rigidbody.AddForce(transform.forward * 20f + transform.up * 10.0f, ForceMode.VelocityChange);
                //TODO:더 좋은 방법 찾아보기
                Invoke(nameof(DisableRiding), 2.5f); // 2.5초 후에 비활성화
            }
        }
    }

    private void DisableRiding()
    {
        m_IsRiding = false;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void CameraLook()
    {
        m_CamCurXRot += mouseDelta.y * m_lookSensitivity;
        m_CamCurXRot = Mathf.Clamp(m_CamCurXRot, m_MinXLook, m_MaxXLook);
        m_CameraContainer.localEulerAngles = new Vector3(-m_CamCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * m_lookSensitivity, 0);
    }

    //아래쪽 체크
    public bool IsCheckDown(LayerMask layerMask)
    {
        Ray[] rays = new Ray[2]
        {
            new Ray(transform.position + (transform.right * 0.1f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.1f) + (transform.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, layerMask))
            {
                return true;
            }
        }
        return false;
    }

    //벽 체크
    private bool IsCheckWall()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.35f, transform.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.5f, Color.red);
        return Physics.Raycast(ray, 0.5f, m_WallMask);
    }

    //대쉬
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !IsDash && IsMove)
        {
            IsDash = true;
            MoveSpeed += DashAddSpeed;
        }
        else if (context.phase == InputActionPhase.Canceled && IsMove)
        {
            IsDash = false;
            MoveSpeed -= DashAddSpeed;
        }
    }

    //벽 이동
    public void OnWallMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // 벽이 감지되고 벽을 잡지않았다면
            if (IsCheckWall() && !IsWallMove)
            {
                IsWallMove = true;
                m_Rigidbody.velocity = Vector3.zero;   //이동 정지
                m_Rigidbody.useGravity = false;   //키를 안눌렀을때 미끄러짐을 방지하기위해 중력 비활성화
            }
            else if (IsWallMove)
            {
                IsWallMove = false;
                m_Rigidbody.useGravity = true;   //중력 활성화
            }
        }
    }

    //벽 이동
    private void WallMove()
    {
        if (IsWallMove)
        {
            if (Input.GetKey(KeyCode.W)) // 위로 이동
            {
                m_Rigidbody.velocity = new Vector3(0, 3.0f, 0);
            }
            else if (Input.GetKey(KeyCode.S)) // 아래로 이동
            {
                m_Rigidbody.velocity = new Vector3(0, -3.0f, 0);
            }
            else // 키를 떼면 정지
            {
                m_Rigidbody.velocity = Vector3.zero;
            }
        }
    }

    //발사기 탑승
    public void RideLancher()
    {
        m_IsRiding = true;
        m_MovementInput = Vector2.zero;
        m_Rigidbody.useGravity = false;
        m_Rigidbody.velocity = Vector3.zero; // 속도 초기화
        m_Rigidbody.angularVelocity = Vector3.zero; // 회전 속도 초기화
    }

    /// 플레이어 넉백
    public void Knockback()
    {
        m_MovementInput = Vector2.zero;
        Vector3 knockbackDirection = (-transform.forward * 1.5f + Vector3.up * 0.5f).normalized;
        Vector3 targetPosition = m_Rigidbody.position + knockbackDirection * 2f; // 목표 위치 설정
        StartCoroutine(IKnockback(m_Rigidbody, targetPosition, 0.5f)); // 0.5초 동안 서서히 이동
    }

    //넉백을 부드럽게
    private IEnumerator IKnockback(Rigidbody rb, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = rb.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime * 2.0f;
            rb.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);   //부드럽게 이동
            yield return null;
        }
        m_Rigidbody.useGravity = true;
    }
}
