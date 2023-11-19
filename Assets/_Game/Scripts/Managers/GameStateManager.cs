using System;
using AgentSystem;
using Sirenix.OdinInspector;
using StateSystem;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public static Action OnGameFinished;
    public bool IsGameFinished { get; private set; }
    
    [SerializeField] 
    private AgentController m_InitialAgent;
    [SerializeField, ReadOnly]
    public StateMachine StateMachine;
    private int m_CurrentHackedAmount = 0;
    public float GameStartTime;
    public int GameTime => (int)(Time.time - GameStartTime);
    private float m_LastJumpTime;


    protected override void OnEnable()
    {
        if (StateMachine)
            StateMachine.OnStateChanged += onStateChanged;
        InputManager.OnSpecialVisionKeyDown += onSpecialVisionKeyDown;
        InputManager.OnSpecialVisionKeyUp += onSpecialVisionKeyUp;
        InputManager.OnMouseUp += onMouseUp;
        AgentController.OnHacked += OnAgentHacked;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (StateMachine)
            StateMachine.OnStateChanged -= onStateChanged;
        InputManager.OnSpecialVisionKeyDown -= onSpecialVisionKeyDown;
        InputManager.OnSpecialVisionKeyUp -= onSpecialVisionKeyUp;
        InputManager.OnMouseUp -= onMouseUp;
        AgentController.OnHacked -= OnAgentHacked;
    }

    public void OnAgentHacked()
    {
        ++m_CurrentHackedAmount;
        MenuManager.Instance.SetProgressBar(m_CurrentHackedAmount, GameConfig.Instance.TargetHackAmount);
        MenuManager.Instance.SetTargetHackText(m_CurrentHackedAmount);
        if (m_CurrentHackedAmount >= GameConfig.Instance.TargetHackAmount)
        {
            var highScore = PlayerPrefs.GetInt("Highscore", 10000);
            var currentScore = GameTime;
            if (currentScore < highScore)
                PlayerPrefs.SetInt("Highscore", currentScore);
            IsGameFinished = true;
            OnGameFinished.InvokeSafe();
            MenuManager.Instance.SetFinishScreen();
        }
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

    [Button]
    public void StartGame()
    {
        var robots = FindObjectsOfType<AgentController>();
        AgentController chosenAgent = null;
        float closestDistance = 1000;
        foreach (var agentController in robots)
        {
            var distance = Vector3.Distance(Vector3.zero, agentController.transform.position); 
            if (distance < closestDistance)
            {
                chosenAgent = agentController;
                closestDistance = distance;
            }
        }
        PlayerManager.Instance.SetPlayerTarget(chosenAgent);
        MenuManager.Instance.SetTargetHackText(m_CurrentHackedAmount);
    }

    public void SetLastJumpTime()
    {
        m_LastJumpTime = Time.time;
    }

    private void onStateChanged(string i_State)
    {
    }
    
    private void onSpecialVisionKeyDown()
    {
        if (StateMachine.CurrentState.GetType() == typeof(PlayerTransitionState))
            return;
        if (Time.time - m_LastJumpTime < GameConfig.Instance.JumpCooldown)
            return;
        StateMachine.SetNewState(nameof(SpecialVisionState));
    }
    
    private void onSpecialVisionKeyUp()
    {
        if (StateMachine.CurrentState.GetType() == typeof(SpecialVisionState))
            StateMachine.SetNewState(nameof(PlayerTransitionState));
    }

    private void onMouseUp()
    {
        if (StateMachine.CurrentState.GetType() == typeof(SpecialVisionState))
            StateMachine.SetNewState(nameof(PlayerTransitionState));
    }
}