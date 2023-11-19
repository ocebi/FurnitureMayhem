using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private BoxCollider m_Collider;
    [SerializeField, ReadOnly] 
    private MysteryBoxHighlightController m_HighlightController;

    private void SetRefs()
    {
        m_Collider = GetComponent<BoxCollider>();
        m_HighlightController = GetComponent<MysteryBoxHighlightController>();
    }

    private void OnValidate()
    {
        SetRefs();
    }

    private void OnEnable()
    {
        // if (!GameStateManager.IsInstanceNull)
            GameStateManager.Instance.StateMachine.OnStateChanged += onStateChanged;
    }

    private void OnDisable()
    {
        if (!GameStateManager.IsInstanceNull)
            GameStateManager.Instance.StateMachine.OnStateChanged -= onStateChanged;
    }

    public GameObject OpenBox()
    {
        m_Collider.enabled = false;
        Invoke(nameof(DestroySelf), 0.1f);
        return Instantiate(GameConfig.Instance.RobotPrefabs.GetRandomItem(), transform.position, Quaternion.identity);
    }
    
    private void onStateChanged(string i_State)
    {
        if (i_State == nameof(SpecialVisionState))
        {
            m_HighlightController.SetSoulTargetHighlight();
        }
        else if (i_State == nameof(PlayerTransitionState) || i_State == nameof(GameplayState))
            m_HighlightController.DisableAllHighlight();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
