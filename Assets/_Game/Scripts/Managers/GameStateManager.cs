using AgentSystem;
using Sirenix.OdinInspector;
using StateSystem;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    //TODO: Initial spawn logic should be refactored
    [SerializeField] 
    private AgentController m_InitialAgent;
    
    [SerializeField, ReadOnly]
    public StateMachine StateMachine;

    protected override void OnEnable()
    {
        StateMachine.OnStateChanged += onStateChanged;
        InputManager.OnSpecialVisionKeyDown += onSpecialVisionKeyDown;
        InputManager.OnSpecialVisionKeyUp += onSpecialVisionKeyUp;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        StateMachine.OnStateChanged -= onStateChanged;
        InputManager.OnSpecialVisionKeyDown -= onSpecialVisionKeyDown;
        InputManager.OnSpecialVisionKeyUp -= onSpecialVisionKeyUp;
    }

    private void OnValidate()
    {
        setRefs();
    }

    [Button]
    private void setRefs()
    {
        if (StateMachine == null)
            StateMachine = GetComponentInChildren<StateMachine>();
    }

    public override void Start()
    {
        PlayerManager.Instance.SetPlayerTarget(m_InitialAgent);
    }

    private void onStateChanged(string i_State)
    {
    }
    
    private void onSpecialVisionKeyDown()
    {
        if (StateMachine.CurrentState.GetType() == typeof(PlayerTransitionState))
            return;
        
        StateMachine.SetNewState(nameof(SpecialVisionState));
    }
    
    private void onSpecialVisionKeyUp()
    {
        if (StateMachine.CurrentState.GetType() == typeof(SpecialVisionState))
            StateMachine.SetNewState(nameof(PlayerTransitionState));
    }
}