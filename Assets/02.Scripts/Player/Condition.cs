using System;
using UnityEngine;

public class Condition
{
    public Condition(float max)
    {
        Max = max;
        m_Current = max;
    }

    public float Max { get; set; }

    public float Current
    {
        get => m_Current;
        set
        {
            m_Current = value;
            OnChangedConditionEvent?.Invoke(m_Current);
        }
    }

    private float m_Current;

    public Action<float> OnChangedConditionEvent = delegate { }; //Current값이 갱신됐을때 호출되는 함수

    public void Add(float amount)
    {
        Current = Mathf.Min(Current + amount, Max);
    }

    public void Subtract(float amount)
    {
        Current = Mathf.Max(Current - amount, 0);
    }
}
