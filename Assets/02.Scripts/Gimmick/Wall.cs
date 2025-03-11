using UnityEngine;

public class Wall : MonoBehaviour, IInteractable
{
    public string GetItemInfo()
    {
        return "Ctrl를 눌러서 벽잡기";
    }
}
