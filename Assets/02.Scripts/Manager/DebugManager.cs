using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = GameManager.Instance;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F1))
        {
            m_GameManager.Player.Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            m_GameManager.Player.Heal(10);
        }
#endif
    }
}
