using System;
using AgentSystem;
using AISystem;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AgentInputController))]
public class AgentController : MonoBehaviour
{
    public static Action OnHacked;
    public Action OnControlTaken;
    public bool HasSoul => m_HasSoul;
    public bool IsHacked => m_HasSoul || m_IsHacked;
    [SerializeField, ReadOnly]
    private bool m_HasSoul;
    private bool m_IsHacked;
    
    [SerializeField] private float m_Speed = 5;

    [SerializeField, ReadOnly]
    private AgentInputController m_AgentInputController;
    [SerializeField, ReadOnly] 
    private AttackController m_AttackController;
    [SerializeField, ReadOnly] 
    private AnimatorController m_AnimatorController;
    [SerializeField, ReadOnly]
    private HighlightController m_HighlightController;
    [SerializeField, ReadOnly]
    private AgentMovement m_AgentMovement;
    [SerializeField, ReadOnly]
    private AgentRotate m_AgentRotate;
    [SerializeField, ReadOnly] 
    private Collector m_Collector;
    private AgentPhysicController m_AgentPhysicsController;
    [SerializeField, ReadOnly] 
    private ReplenishableUI m_ReplenishableUI;
    [SerializeField, ReadOnly] 
    private Bot m_Bot;
    [SerializeField, ReadOnly] 
    private Health m_Health;
    [SerializeField, ReadOnly] 
    private PunchScaleFeedback m_PunchScaleFeedback;
    [ReadOnly] 
    public Transform VisualTransform;
    [SerializeField] 
    private eSoundType m_AttackSound;

    [SerializeField] GameObject deathParticlePrefab;

    [Button]
    private void setRefs()
    {
        m_AgentInputController = GetComponent<AgentInputController>();
        VisualTransform = transform.GetChild(0).transform;
        m_AgentMovement = GetComponent<AgentMovement>();
        m_AgentRotate = GetComponent<AgentRotate>();
        m_HighlightController = GetComponent<HighlightController>();
        m_AttackController = GetComponent<AttackController>();
        m_Collector = GetComponentInChildren<Collector>();
        m_AnimatorController = GetComponent<AnimatorController>();
        m_ReplenishableUI = GetComponentInChildren<ReplenishableUI>();
        m_Bot = GetComponent<Bot>();
        m_Health = GetComponent<Health>();
        m_PunchScaleFeedback = GetComponent<PunchScaleFeedback>();
    }

    private void OnValidate()
    {
        setRefs();
    }

    private void Awake()
    {
        m_AgentPhysicsController = new AgentPhysicController(gameObject);
    }

    private void OnEnable()
    {
        if (!GameStateManager.IsInstanceNull)
            GameStateManager.Instance.StateMachine.OnStateChanged += onStateChanged;
        m_AgentInputController.OnMovementUp += OnMovementUp;
        m_AgentInputController.OnAttack += Attack;
        m_Collector.OnCollectTargetReached += OnCollectTargetReached;
        m_Health.OnValueChanged += OnHealthChanged;
        m_Health.OnValueBelowZero += OnHealthBelowZero;
        GameStateManager.OnGameFinished += OnGameFinished;
    }

    private void OnDisable()
    {
        m_AgentInputController.OnMovementUp -= OnMovementUp;
        m_AgentInputController.OnAttack -= Attack;
        m_Collector.OnCollectTargetReached -= OnCollectTargetReached;
        m_Health.OnValueChanged -= OnHealthChanged;
        m_Health.OnValueBelowZero -= OnHealthBelowZero;
        GameStateManager.OnGameFinished -= OnGameFinished;
        if (!GameStateManager.IsInstanceNull)
            GameStateManager.Instance.StateMachine.OnStateChanged -= onStateChanged;
    }

    private void OnGameFinished()
    {
        if (HasSoul)
            m_Health.IsInvulnerable = true;
        OnMovementUp();
    }

    private void FixedUpdate()
    {
        if (GameStateManager.Instance.IsGameFinished)
            return;
        
        if (m_AgentInputController.MovementInput != Vector3.zero)
        {
            m_AgentMovement.MoveCharacter(m_AgentInputController.MovementInput);
            m_AgentRotate.Rotate(m_AgentInputController.MovementInput);
        }
    }
    
    private void OnCollectTargetReached()
    {
        SetHacked();
    }

    private void OnMovementUp()
    {
        m_AgentMovement.MoveCharacter(Vector2.zero);
    }

    public void SetLocalControl(bool i_Value)
    {
        m_AgentInputController.SetInputAuthority(i_Value);
        if (!i_Value)
        {
            m_Bot.Initialize();
            m_AgentInputController.SetMoveInput(Vector2.zero);
            if (m_IsHacked)
                m_ReplenishableUI.SetBackgroundColor(Color.green);
            else
                m_ReplenishableUI.SetBackgroundColor(Color.red);
        }
        else
        {
            m_Bot.ResetAI();
            m_ReplenishableUI.SetBackgroundColor(Color.yellow);
            OnControlTaken.InvokeSafe();
        }
        m_HasSoul = i_Value;
    }

    public void SetHacked()
    {
        m_IsHacked = true;
        m_HighlightController.SetHackedHighlight();
        OnHacked.InvokeSafe();
    }
    
    private void onStateChanged(string i_State)
    {
        if (i_State == nameof(SpecialVisionState))
        {
            m_HighlightController.SetSoulTargetHighlight();
            if (HasSoul)
                m_HighlightController.SetTransitionHighlight();
        }
        else if (i_State == nameof(PlayerTransitionState) || i_State == nameof(GameplayState))
            m_HighlightController.DisableAllHighlight();
    }

    private void Attack()
    {
        if (!m_AttackController.IsCooldownFinished ||
            GameStateManager.Instance.IsGameFinished)
            return;
        
        Ray ray = CameraManager.Instance.Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            CancelInvoke(nameof(EnableRotate));
            m_AgentRotate.ToggleRotate(false);
            var lookPos = hit.point;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos, Vector3.up);
            var attackDirection = (lookPos - transform.position).normalized;
            m_AttackController.Attack(attackDirection);
            m_AnimatorController.PlayAttack();
            SoundManager.Instance.PlaySound(m_AttackSound);
            Invoke(nameof(EnableRotate), 0.5f);
        }
    }

    private void EnableRotate()
    {
        m_AgentRotate.ToggleRotate(true);
    }
    
    private void OnHealthChanged()
    {
        m_PunchScaleFeedback.Play();
    }

    private void OnHealthBelowZero()
    {
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}