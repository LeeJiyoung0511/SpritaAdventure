using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float CurrentHp
    {
        get => m_CurrentHp;
        set
        {
            m_CurrentHp = value;
            OnChangedHpEvent?.Invoke(m_CurrentHp);
        }
    }

    private float m_CurrentHp;

    public float MaxHp = 100;

    public Action<float> OnChangedHpEvent = delegate { };

    public bool IsDead => CurrentHp == 0;

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
    }

    private void Start()
    {
        m_CurrentHp = MaxHp;
        m_GameManager.UIManager.HPBar.Set(MaxHp);
    }

    //체력회복
    public void Heal(float amount)
    {
        CurrentHp = Mathf.Min(CurrentHp + amount, MaxHp);        
    }

    //체력감소
    public void Damage(float amount)
    {
        CurrentHp = Mathf.Max(CurrentHp - amount, 0);
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
