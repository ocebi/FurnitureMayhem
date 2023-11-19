using System;
using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using Sirenix.OdinInspector;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public eCollectable CollectableType => m_CollectableType;
    
    [SerializeField]
    private eCollectable m_CollectableType;

    [SerializeField, ReadOnly] 
    private GameObject m_Model;

    [SerializeField] 
    private HighlightEffect m_HighlightEffect;

    private bool m_IsCollected;

    private void SetRefs()
    {
        m_Model = transform.FindDeepChild<GameObject>("Model");
        m_HighlightEffect = GetComponentInChildren<HighlightEffect>();
    }

    private void OnValidate()
    {
        SetRefs();
    }

    private void OnEnable()
    {
        AgentController.OnRobotControlTaken += OnControlTaken;
    }

    private void OnDisable()
    {
        AgentController.OnRobotControlTaken -= OnControlTaken;
    }

    public void Collect()
    {
        if (m_IsCollected)
            return;
        m_Model.SetActive(false);
        Invoke(nameof(Respawn), 10f);
        // Destroy(gameObject);
    }

    private void Respawn()
    {
        m_Model.SetActive(true);
        m_IsCollected = false;
    }

    private void OnControlTaken(AgentController agentController)
    {
        if (agentController.CollectableType == m_CollectableType)
        {
            m_HighlightEffect.SetHighlighted(true);
        }
        else
        {
            m_HighlightEffect.SetHighlighted(false);
        }
    }
}