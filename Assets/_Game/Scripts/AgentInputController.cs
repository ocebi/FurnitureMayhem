using System;
using AgentSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class AgentInputController : MonoBehaviour
{
    #region Input Actions
    public Action OnMovementDown;
    public Action<Vector2> OnMovement;
    public Action OnMovementUp;
    public Action OnAttack;
    #endregion

    public Vector3 MovementInput => m_MovementInput;
    [SerializeField, ReadOnly]
    private Vector3 m_MovementInput;
    [SerializeField, ReadOnly]
    private AgentController m_AgentController;
    [SerializeField, ReadOnly]
    private bool m_UsePlayerInput;
    
    [Button]
    private void setRefs()
    {
        m_AgentController = GetComponent<AgentController>();
    }

    private void OnDisable()
    {
        disablePlayerInput();
    }

    #region Unity Methods

    private void Update()
    {
        if (!m_UsePlayerInput)
            return;
        
        if (m_MovementInput != Vector3.zero)
        {
            var newMovement = cameraRelativeFlatten(InputManager.Instance.MovementInput, transform.up);
            if (newMovement == Vector3.zero)
                OnMovementUp.InvokeSafe();
        }
        if (m_UsePlayerInput && InputManager.Instance.IsMovementInputDown)
        {
            m_MovementInput = cameraRelativeFlatten(InputManager.Instance.MovementInput, transform.up);
            setMoveInput(m_MovementInput);
        }
        else if (m_UsePlayerInput && !InputManager.Instance.IsMovementInputDown)
        {
            m_MovementInput = Vector3.zero;
        }

    }

    #endregion

    #region Public Methods
    
    public void SetInputAuthority(bool i_Value)
    {
        if (i_Value)
            enablePlayerInput();
        else
            disablePlayerInput();
    }

    public void SetMoveInput(Vector2 i_Movement)
    {
        if (!m_UsePlayerInput)
        {
            setMoveInput(i_Movement);
        }
        else
            Debug.LogError("Move should only be used by AI", gameObject);
    }

    public void SetAttackInput()
    {
        if (!m_UsePlayerInput)
            setAttackInput();
        else
            Debug.LogError("Attack should only be used by AI");
    }

    #endregion

    #region Private Methods

    private void setMoveInput(Vector2 i_Movement)
    {
        if (i_Movement == Vector2.zero && m_MovementInput != Vector3.zero)
            OnMovementUp.InvokeSafe();
        OnMovement.InvokeSafe(i_Movement);
        m_MovementInput = i_Movement.normalized;
    }
    
    private void setAttackInput()
    {
        OnAttack.InvokeSafe();
    }

    private void enablePlayerInput()
    {
        if (!m_UsePlayerInput)
        {
            // InputManager.OnMovement += move;
            InputManager.OnAttack += setAttackInput;
            m_UsePlayerInput = true;
        }
    }

    private void disablePlayerInput()
    {
        if (m_UsePlayerInput)
        {
            // InputManager.OnMovement -= move;
            InputManager.OnAttack -= setAttackInput;
            m_MovementInput = Vector3.zero;
            m_UsePlayerInput = false;
        }
        OnMovementUp.InvokeSafe();
    }
    
    private Vector3 cameraRelativeFlatten(Vector3 input, Vector3 localUp)
    {
        Transform cam = Camera.main.transform; // You can cache this to save a search.

        Quaternion flatten = Quaternion.LookRotation(
                                 -localUp, 
                                 cam.forward
                             )
                             * Quaternion.Euler(-90f, 0, 0);

        return flatten * input;
    }

    #endregion
}
