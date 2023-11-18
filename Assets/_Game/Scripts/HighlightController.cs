using System;
using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class HighlightController : MonoBehaviour
{
    [SerializeField]
    private HighlightEffect m_SoulTargetHighlight;
    [SerializeField]
    private HighlightEffect m_SelectedHighlight;
    [SerializeField, ReadOnly] 
    private AgentController m_AgentController;
    // [SerializeField, ReadOnly] 
    // private LineRenderer m_LineRenderer;
    [SerializeField]
    private VisualEffect m_VisualEffect;
    [SerializeField, ReadOnly]
    private bool m_IsHighlightActive;

    [Button]
    private void setRefs()
    {
        m_AgentController = GetComponent<AgentController>();
        // m_LineRenderer = transform.FindDeepChild<LineRenderer>("LineRenderer");
    }

    private void OnValidate()
    {
        setRefs();
    }

    private void LateUpdate()
    {
        if (m_IsHighlightActive)
        {
            SetTrajectory(PlayerManager.Instance.ActiveAgent.transform);
            // Soul.Instance.ActiveAgent?.SetTrajectory(transform);
        }
        else
        {
            // m_LineRenderer.gameObject.SetActive(false);
            m_VisualEffect.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        // m_LineRenderer.gameObject.SetActive(false);
        m_VisualEffect.gameObject.SetActive(false); 
    }

    private void OnMouseEnter()
    {
        if (m_AgentController.HasSoul)
            return;

        if (GameStateManager.Instance.StateMachine.CurrentState.GetType() == typeof(SpecialVisionState))
            SetSelectedHighlight();
    }

    private void OnMouseExit()
    {
        if (GameStateManager.Instance.StateMachine.CurrentState.GetType() == typeof(SpecialVisionState))
            SetSoulTargetHighlight();
    }

    [Button]
    public void SetSoulTargetHighlight()
    {
        m_SelectedHighlight.SetHighlighted(false);
        m_SoulTargetHighlight.SetHighlighted(true);    
        m_IsHighlightActive = false;
    }

    [Button]
    public void SetSelectedHighlight()
    {
        m_SoulTargetHighlight.SetHighlighted(false);
        m_SelectedHighlight.SetHighlighted(true);
        m_IsHighlightActive = true;
    }

    [Button]
    public void DisableAllHighlight()
    {
        m_SoulTargetHighlight.SetHighlighted(false);
        m_SelectedHighlight.SetHighlighted(false);
        m_IsHighlightActive = false;
    }

    public void SetTrajectory(Transform m_TargetTransform)
    {
        // m_LineRenderer.positionCount = 2;
        // m_LineRenderer.SetPosition(0, transform.position + Vector3.up * 0.5f);
        // m_LineRenderer.SetPosition(1, m_TargetTransform.position + Vector3.up * 0.5f);
        // m_LineRenderer.gameObject.SetActive(true);

        m_VisualEffect.gameObject.SetActive(true);
        m_VisualEffect.SetVector3("StartPos", m_TargetTransform.position + Vector3.up * 0.5f);
        m_VisualEffect.SetVector3("TargetPos", transform.position + Vector3.up * 0.5f);
    }
}
