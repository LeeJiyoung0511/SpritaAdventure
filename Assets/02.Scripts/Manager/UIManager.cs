using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HPBar HPBar { get; set; }

    private void Awake()
    {
        GameManager.Instance.UIManager = this;
    }
}
