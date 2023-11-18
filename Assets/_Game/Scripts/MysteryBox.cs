using Sirenix.OdinInspector;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private BoxCollider m_Collider;

    private void SetRefs()
    {
        m_Collider = GetComponent<BoxCollider>();
    }

    private void OnValidate()
    {
        SetRefs();
    }

    public GameObject OpenBox()
    {
        m_Collider.enabled = false;
        Invoke(nameof(DestroySelf), 0.1f);
        return Instantiate(GameConfig.Instance.RobotPrefabs.GetRandomItem(), transform.position, Quaternion.identity);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
