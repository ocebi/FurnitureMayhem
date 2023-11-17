using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    #region Input Actions
    public static Action OnMovementDown;
    public static Action<Vector2> OnMovement;
    public static Action OnMovementUp;
    public static Action OnJump;
    public static Action OnSpecialVisionKeyDown;
    public static Action OnSpecialVisionKeyUp;
    public static Action OnAttack;
    #endregion
    
    #region Movement Vars
    public bool IsMovementInputDown => m_MovementInput != Vector2.zero;
    public Vector2 MovementInput => m_MovementInput;
    [SerializeField, ReadOnly]
    private Vector2 m_MovementInput;
    #endregion
    
    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var newMovement = new Vector2(horizontal, vertical);
        if (m_MovementInput == Vector2.zero && m_MovementInput != newMovement)
            OnMovementDown.InvokeSafe();
        if (newMovement == Vector2.zero && m_MovementInput != newMovement)
            OnMovementUp.InvokeSafe();
        m_MovementInput = new Vector2(horizontal, vertical);
        // m_MovementInput = m_InputActions.Player.Movement.ReadValue<Vector2>();
        if (m_MovementInput.magnitude > 1)
            m_MovementInput.Normalize();
        if (IsMovementInputDown)
        {
            OnMovement.InvokeSafe(m_MovementInput);
            // Debug.Log($"Movement: {m_MovementInput}");
        }
        if (Input.GetKeyDown(KeyCode.R))
            OnSpecialVisionKeyDown.InvokeSafe();
        else if (Input.GetKeyUp(KeyCode.R))
            OnSpecialVisionKeyUp.InvokeSafe();
        if (Input.GetKeyDown(KeyCode.Space))
            OnJump.InvokeSafe();
        if (Input.GetMouseButtonDown(0))
            OnAttack.InvokeSafe();
    }
}
