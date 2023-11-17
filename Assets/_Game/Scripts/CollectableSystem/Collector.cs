using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Action OnItemCollected;
    public Action OnCollectTargetReached;

    [SerializeField] 
    private eCollectable m_CollectType;
    [SerializeField] 
    private int m_TargetCollectAmount = 5;
    private int m_CurrentCollectAmount;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out var collectable) && 
            collectable.CollectableType == m_CollectType &&
            m_CurrentCollectAmount < m_TargetCollectAmount)
        {
            collectable.Collect();
            ++m_CurrentCollectAmount;
            OnItemCollected.InvokeSafe();
            if (m_CurrentCollectAmount >= m_TargetCollectAmount)
                OnCollectTargetReached.InvokeSafe();
        }
    }
}
