using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("이동")]
    [SerializeField]
    private float m_MoveSpeed = 5.0f;  //이동속도
    private Vector2 m_MovementInput;   //입력받은 이동
    public float m_Drag = 5f;          //감속 (마찰력)
    public float m_Acceleration = 10f; //가속력
    public float m_MaxSpeed = 5f;      //최대 이동 속도

    [Header("점프")]
    [SerializeField]
    private float m_JumpPower = 100f; //점프
    [SerializeField]
    private LayerMask m_GroundMask;

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

        m_Rigidbody.drag = m_Drag;
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
        //이동속도 일정하게 유지
        Vector3 movement = new Vector3(m_MovementInput.x, 0, m_MovementInput.y).normalized;

        // 현재 속도가 maxSpeed 이상이면 힘을 추가하지 않음
        if (m_Rigidbody.velocity.magnitude < m_MaxSpeed)
        {
            m_Rigidbody.AddForce(movement * m_Acceleration, ForceMode.Acceleration);
        }
    }

    //플레이어 이동 입력
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            m_MovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            m_MovementInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsCheckDown(m_GroundMask))
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpPower, ForceMode.Impulse);
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
}
