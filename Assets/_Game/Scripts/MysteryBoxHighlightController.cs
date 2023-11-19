using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;

public class MysteryBoxHighlightController : MonoBehaviour
{
    [SerializeField]
    private HighlightEffect m_SoulTargetHighlight;
    [SerializeField]
    private HighlightEffect m_SelectedHighlight;
    
    private void OnMouseEnter()
    {
        if (GameStateManager.Instance.StateMachine.CurrentState.GetType() == typeof(SpecialVisionState))
            SetSelectedHighlight();
    }

    private void OnMouseExit()
    {
        if (GameStateManager.Instance.StateMachine.CurrentState.GetType() == typeof(SpecialVisionState))
            SetSoulTargetHighlight();
    }
    
    public void SetSoulTargetHighlight()
    {
        m_SelectedHighlight.SetHighlighted(false);
        m_SoulTargetHighlight.SetHighlighted(true);    
    }

    public void SetSelectedHighlight()
    {
        m_SoulTargetHighlight.SetHighlighted(false);
        m_SelectedHighlight.SetHighlighted(true);
    }
    
    public void DisableAllHighlight()
    {
        m_SoulTargetHighlight.SetHighlighted(false);
        m_SelectedHighlight.SetHighlighted(false);
    }
}