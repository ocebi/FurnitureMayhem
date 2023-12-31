using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Action OnItemCollected;
    public Action OnCollectTargetReached;
    public eCollectable CollectType => m_CollectType;
    [SerializeField] 
    private eCollectable m_CollectType;
    [SerializeField] 
    private int m_TargetCollectAmount = 5;
    private int m_CurrentCollectAmount;
    [SerializeField, ReadOnly]
    private AgentController m_AgentController;

    [Button]
    private void SetRefs()
    {
        m_AgentController = GetComponentInParent<AgentController>();
    }

    private void OnValidate()
    {
        SetRefs();
    }
    
    private void Awake()
    {
        SetRefs();
    }

    private void OnEnable()
    {
        m_AgentController.OnControlTaken += OnControlTaken;
    }

    private void OnDisable()
    {
        m_AgentController.OnControlTaken -= OnControlTaken;
    }

    private void OnControlTaken()
    {
        MenuManager.Instance.SetTarget(m_CollectType, m_CurrentCollectAmount, m_TargetCollectAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_AgentController.HasSoul)
            return;
        
        if (other.TryGetComponent<Collectable>(out var collectable) && 
            collectable.CollectableType == m_CollectType &&
            m_CurrentCollectAmount < m_TargetCollectAmount)
        {
            collectable.Collect();
            ++m_CurrentCollectAmount;
            OnItemCollected.InvokeSafe();
            if (m_CurrentCollectAmount >= m_TargetCollectAmount)
                OnCollectTargetReached.InvokeSafe();
            MenuManager.Instance.SetTarget(m_CollectType, m_CurrentCollectAmount, m_TargetCollectAmount);
        }
    }
}
