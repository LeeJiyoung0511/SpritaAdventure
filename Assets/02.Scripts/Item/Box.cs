using UnityEngine;

public class Box : MonoBehaviour, IDropable, IInteractable
{
    [SerializeField]
    private ItemData boxData;
    [SerializeField]
    private Animator m_boxAnim;

    [SerializeField]
    private GameObject m_BoxObject;
    [SerializeField]
    private BoxCollider m_Collider;
    [SerializeField]
    private GameObject m_DestroyBoxObject;

    private bool m_IsDrop = false;

    public void Drop(Vector3 hitPoint, Vector3 hitNormal)
    {
        if (m_IsDrop) return;
        m_IsDrop = true;

        m_Collider.enabled = false;
        m_BoxObject.SetActive(false);
        m_DestroyBoxObject.SetActive(true);

        //상자 파괴 애니메이션 실행
        m_boxAnim.SetBool("Destroy", true);
        //드롭 아이템 생성
        foreach (var item in boxData.DropItemPrefabs)
        {
            Instantiate(item,hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
        //상자 파괴
        Invoke("DestroyBox", 1.0f);
    }

    public string GetItemInfo()
    {
        return "클릭으로 상자 열기";
    }

    private void DestroyBox()
    {
        Destroy(gameObject);
    }
}
