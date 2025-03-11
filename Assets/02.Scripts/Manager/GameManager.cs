using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameObject("GameManager").AddComponent<GameManager>();
                m_Instance.AddComponent<DebugManager>();
            }
            return m_Instance;
        }
    }
    private static GameManager m_Instance;

    public Player Player { get; set; }
    public UIManager UIManager { get; set; }

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MoveStartScene()
    {
        SceneManager.LoadScene("Start");
    }
}
