using System;
using UnityEngine;

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


    private void Awake()
    {
        m_GameManager = GameManager.Instance;
        m_GameManager.Player = this;
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
}
