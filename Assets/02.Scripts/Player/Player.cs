using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float MaxHp;
    public float MaxStamina;

    public Condition Hp;
    public Condition Stamina;

    public bool IsDead => Hp.Current == 0;

    private GameManager m_GameManager;

    public Interaction Interaction { get; private set; }
    public PlayerController Controller { get; private set; }
    public Inventory Inventory { get; private set; }

    [SerializeField]
    private Renderer m_Renderer;
    private Color m_BaseColor; //기초 색상
    private float ColorChangeTime = 0.3f; // 색상 변경 간격

    private void Awake()
    {
        m_GameManager = GameManager.Instance;
        m_GameManager.Player = this;
        Interaction = GetComponent<Interaction>();
        Controller = GetComponent<PlayerController>();
        Inventory = GetComponent<Inventory>();

        Hp = new(MaxHp);
        Stamina = new(MaxStamina);
    }

    private void Start()
    {
        m_GameManager.UIManager.HPBar.Set(MaxHp);
        m_GameManager.UIManager.StaminaBar.Set(MaxStamina);

        m_BaseColor = m_Renderer.material.color;
    }

    private void Update()
    {
        //대쉬중이면
        if (Controller.IsDash)
        {
            //스태미너 감소
            Stamina.Subtract(Time.deltaTime * 5f);
        }
        else
        {
            //스태미너 자동 회복
            Stamina.Add(Time.deltaTime);
        }

        // 체력 0이 되면 시작화면으로 이동
        if (IsDead)
        {
            SceneManager.LoadScene("Start");
        }
    }

    //체력회복
    public void Heal(float amount)
    {
        Hp.Add(amount);
    }

    //체력감소
    public void Damage(float amount)
    {
        Hp.Subtract(amount);
        Controller.Knockback();
    }

    //아이템 사용
    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Inventory.Use(this);
        }
    }

    //무적 활성화
    public void ActivateInvincibility(float duration)
    {
        StartCoroutine(IChangeRandomColor(duration));
    }

    //플레이어 몸 색깔 랜덤 변경
    private IEnumerator IChangeRandomColor(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            m_Renderer.material.color = new Color(Random.value, Random.value, Random.value);
            yield return new WaitForSeconds(ColorChangeTime); // 일정 간격으로 변경
            elapsedTime += ColorChangeTime;
        }
        m_Renderer.material.color = m_BaseColor; // 무적 종료 후 원래 색상 복구
    }
}
