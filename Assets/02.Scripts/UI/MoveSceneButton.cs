using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneButton : MonoBehaviour
{
    public string MoveSceneName; // 이동할 씬 이름

    public void MoveScene()
    {
        SceneManager.LoadScene(MoveSceneName);
    }
}
