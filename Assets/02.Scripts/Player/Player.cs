using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void Update()
    {
        if (Controller.IsDash)
        {
            Stamina.Subtract(Time.deltaTime * 5f);
        }
        else
        {
            Stamina.Add(Time.deltaTime);
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
    }

    //아이템 사용
    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started )
        {
            Inventory.Use(this);
        }
    }
}
