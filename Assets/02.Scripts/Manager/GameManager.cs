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
        }
    }

    //시작화면으로 이동
    public void MoveStartScene()
    {
        SceneManager.LoadScene("Start");
    }
}
