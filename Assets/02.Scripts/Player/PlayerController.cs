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

    [Header("시선")]
    [SerializeField]
    private Transform m_CameraContainer;
    private float m_MinXLook = -3f;  // 최소 시야각
    private float m_MaxXLook = 3.0f;  // 최대 시야각
    private float m_CamCurXRot; //현재카메라
    private float m_lookSensitivity = 0.1f; // 카메라 민감도
    private Vector2 mouseDelta;  // 마우스 변화값

    private Rigidbody m_Rigidbody;
    private Animator m_Animator;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
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
        }
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
}
