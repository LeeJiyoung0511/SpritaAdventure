using UnityEngine;
using UnityEngine.UI;

public class BarBase : MonoBehaviour
{
    private Slider m_Bar;

    protected virtual void Start()
    {
        m_Bar = GetComponent<Slider>();
    }

    //바 값 설정
    public void Set(float maxHp)
    {
        m_Bar.maxValue = maxHp;
        m_Bar.value = maxHp;
    }

    //바 값 갱신
    protected void UpdateBar(float current)
    {
        m_Bar.value = current;
    }
}
