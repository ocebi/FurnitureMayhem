using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputListener : MonoBehaviour
{
    public Action<Vector2> OnMoveHold;
    public Action OnMoveReleased;
    //public Action<Vector2> OnWeaponAiming;
    //public Action<Vector2> OnBombAiming;
    //public Action OnJump;
    //public Action OnWeaponReleased;
    //public Action OnBombReleased;
    //public Action OnSkillUsed;
   

    public abstract void SetInputs(Controller controller);

    public abstract void RemoveInputs(Controller controller);
    

    protected virtual void CallOnMove(Vector2 value)
    {
        OnMoveHold?.Invoke(value);
    }
    //protected virtual void CallOnWeaponAiming(Vector2 value)
    //{
    //    OnWeaponAiming?.Invoke(value);
    //}
    //protected virtual void CallOnBombAiming(Vector2 value)
    //{
    //    OnBombAiming?.Invoke(value);
    //}
    //protected virtual void CallOnSkillUsed()
    //{
    //    OnSkillUsed.Invoke();
    //}
    //protected virtual void CallOnJump()
    //{
    //    OnJump?.Invoke();
    //}
    protected virtual void CallOnMoveReleased()
    {
        OnMoveReleased?.Invoke();
    }
    //protected virtual void CallOnWeaponReleased()
    //{
    //    OnWeaponReleased?.Invoke();
    //}
    //protected virtual void CallOnBombReleased()
    //{
    //    OnBombReleased?.Invoke();
    //}

}