using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

public class ReplenishableUI : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private Replenishable m_Replenishable;
    [SerializeField, ReadOnly]
    private MMProgressBar m_ProgressBar;
    
    [Button]
    private void setRefs()
    {
        m_Replenishable = GetComponentInParent<Health>();
        m_ProgressBar = GetComponentInChildren<MMProgressBar>();
    }

    private void OnValidate()
    {
        setRefs();
    }

    private void Awake()
    {
        m_ProgressBar.SetBar(m_Replenishable.MaxValue, 0, m_Replenishable.MaxValue);
    }

    private void OnEnable()
    {
        m_Replenishable.OnValueChanged += onHealthChanged;
    }

    private void OnDisable()
    {
        m_Replenishable.OnValueChanged -= onHealthChanged;
    }

    private void onHealthChanged()
    {
        m_ProgressBar.UpdateBar(m_Replenishable.CurrentValue, 0, m_Replenishable.MaxValue);
    }
}