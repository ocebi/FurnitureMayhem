using System;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Replenishable : MonoBehaviour
{
    #region Actions
    public Action OnValueBelowZero;
    public Action OnValueChanged;
    #endregion

    #region Public Variables
    public int InitialValue => m_InitialValue;
    public int CurrentValue { get { return m_CurrentValue; } protected set { m_CurrentValue = value; } }
    public int MaxValue => m_MaxValue;
    public float Percentage { get { return m_MaxValue == 0 ? 0 : 1f * CurrentValue / m_MaxValue; } }
    public bool IsDepleted => CurrentValue == 0;
    #endregion
    
    [SerializeField, Min(1)]
    private int m_InitialValue = 100;
    [SerializeField, ReadOnly]
    protected int m_MaxValue = 500;
    [SerializeField, ReadOnly]
    private int m_CurrentValue;

    #region Unity Methods

    protected virtual void OnValidate()
    {
        SetInitialValue(m_InitialValue);
    }

    #endregion

    #region Class Methods

    [Button]
    public virtual void IncreaseValue(int i_Value)
    {
        if (i_Value <= 0)
            return;
        if (IsDepleted)
            return;
        SetNewValue(CurrentValue + i_Value);
    }

    [Button]
    public virtual void DecreaseValue(int i_Value)
    {
        if (i_Value <= 0)
            return;
        if (IsDepleted)
            return;
        SetNewValue(CurrentValue - i_Value);
    }
    
    [Button]
    public virtual void SetInitialValue(int i_Value)
    {
        SetMaxValue(i_Value);
        SetNewValue(i_Value);
    }

    public void SetMaxValue(int i_Value)
    {
        m_MaxValue = i_Value;
        SetNewValue(m_MaxValue);
    }

    protected virtual void SetNewValue(int i_Value)
    {
        var oldValue = CurrentValue;
        CurrentValue = Mathf.Clamp(i_Value, 0, MaxValue);
        if (CurrentValue != oldValue)
            OnValueChanged.InvokeSafe();
        if (IsDepleted)
            OnValueBelowZero.InvokeSafe();
    }
    #endregion
}