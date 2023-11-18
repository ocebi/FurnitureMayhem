using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ReplenishableUI : MonoBehaviour
{
    [SerializeField, ReadOnly] 
    private AgentController m_AgentController;
    [SerializeField, ReadOnly]
    private Replenishable m_Replenishable;
    [SerializeField, ReadOnly]
    private MMProgressBar m_ProgressBar;
    [SerializeField, ReadOnly] 
    private Image m_BarFront;
    [SerializeField, ReadOnly] 
    private Image m_DelayedBarDecreasing;
    
    [Button]
    private void setRefs()
    {
        m_Replenishable = GetComponentInParent<Health>();
        m_ProgressBar = GetComponentInChildren<MMProgressBar>();
        m_BarFront = transform.FindDeepChild<Image>("BarFront");
        m_DelayedBarDecreasing = transform.FindDeepChild<Image>("DelayedBarDecreasing");
        m_AgentController = GetComponentInParent<AgentController>();
    }

    private void OnValidate()
    {
        setRefs();
    }

    private void Awake()
    {
        setRefs();
        m_ProgressBar.TimeScale = MMProgressBar.TimeScales.UnscaledTime;
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

    public void SetBackgroundColor(Color color)
    {
        m_DelayedBarDecreasing.color = color;
        m_BarFront.color = color;
    }

    private void onHealthChanged()
    {
        m_ProgressBar.UpdateBar(m_Replenishable.CurrentValue, 0, m_Replenishable.MaxValue);
    }
    
    
}