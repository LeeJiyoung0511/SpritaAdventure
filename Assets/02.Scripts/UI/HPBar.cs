using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Slider m_HpBar;

    private void Start()
    {
        m_HpBar = GetComponent<Slider>();
        GameManager.Instance.Player.OnChangedHpEvent += UpdateHpBar;
    }

    //체력바 값 설정
    public void Set(float maxHp)
    {
        m_HpBar.maxValue = maxHp;
        m_HpBar.value = maxHp;
    }

    //체력바 값 갱신
    private void UpdateHpBar(float currentHp)
    {
        m_HpBar.value = currentHp;
    }
}
