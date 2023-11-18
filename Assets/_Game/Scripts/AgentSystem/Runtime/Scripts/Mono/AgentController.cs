using System;
using AgentSystem;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AgentInputController))]
public class AgentController : MonoBehaviour
{
    public bool HasSoul => m_HasSoul;
    public bool IsHacked;
    [SerializeField, ReadOnly]
    private bool m_HasSoul;
    
    [SerializeField] private float m_Speed = 5;

    [SerializeField, ReadOnly]
    private AgentInputController m_AgentInputController;
    [SerializeField, ReadOnly] 
    private AttackController m_AttackController;
    [SerializeField, ReadOnly]
    private HighlightController m_HighlightController;

    private AgentMovement m_AgentMovement;
    private AgentRotate m_AgentRotate;
    private AgentPhysicController m_AgentPhysicsController;

    [ReadOnly] 
    public Transform VisualTransform;

    [Button]
    private void setRefs()
    {
        m_AgentInputController = GetComponent<AgentInputController>();
        VisualTransform = transform.GetChild(0).transform;
        m_AgentMovement = GetComponent<AgentMovement>();
        m_AgentRotate = GetComponent<AgentRotate>();
        m_HighlightController = GetComponent<HighlightController>();
        m_AttackController = GetComponent<AttackController>();
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
        m_AgentInputController.OnJump += onJump;
        GameStateManager.Instance.StateMachine.OnStateChanged += onStateChanged;
        m_AgentInputController.OnMovementUp += OnMovementUp;
        m_AgentInputController.OnAttack += Attack;
    }

    private void OnDisable()
    {
        m_AgentInputController.OnJump -= onJump;
        m_AgentInputController.OnMovementUp -= OnMovementUp;
        m_AgentInputController.OnAttack -= Attack;
        if (GameStateManager.Instance)
            GameStateManager.Instance.StateMachine.OnStateChanged -= onStateChanged;
    }

    private void FixedUpdate()
    {        
        if (m_AgentInputController.MovementInput != Vector3.zero)
        {
            m_AgentMovement.MoveCharacter(m_AgentInputController.MovementInput);
            m_AgentRotate.Rotate(m_AgentInputController.MovementInput);
        }
    }
    
    private void onJump()
    {
        //TODO: Implement jump
        // if (!m_CharacterActor.IsGrounded)
        //     return;
        // // Debug.LogError("Jump");
        //
        // m_CharacterActor.ForceNotGrounded();
        // m_CharacterActor.VerticalVelocity = m_CharacterActor.Up * 7.5f;
    }

    private void OnMovementUp()
    {
        m_AgentMovement.MoveCharacter(Vector2.zero);
    }

    public void SetLocalControl(bool i_Value)
    {
        m_AgentInputController.SetInputAuthority(i_Value);
        m_HasSoul = i_Value;
        IsHacked = i_Value; //TODO: Shouldn't be set to false when jumping out of hacked character
    }
    
    private void onStateChanged(string i_State)
    {
        if (i_State == nameof(SpecialVisionState))
            m_HighlightController.SetSoulTargetHighlight();
        else if (i_State == nameof(PlayerTransitionState) || i_State == nameof(GameplayState))
            m_HighlightController.DisableAllHighlight();
    }

    private void Attack()
    {
        if (!m_AttackController.IsCooldownFinished)
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
            Invoke(nameof(EnableRotate), 0.5f);
        }
    }

    private void EnableRotate()
    {
        m_AgentRotate.ToggleRotate(true);
    }
}