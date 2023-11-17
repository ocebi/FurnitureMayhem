using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputListener : InputListener
{
    // private void OnEnable()
    // {
    //     UIInputManager.OnMoving += CallOnMove;
    //     UIInputManager.OnMoveJoystickReleased += CallOnMoveReleased;
    // }
    //
    // private void OnDisable()
    // {
    //     UIInputManager.OnMoving -= CallOnMove;
    //     UIInputManager.OnMoveJoystickReleased -= CallOnMoveReleased;
    // }

    protected override void CallOnMove(Vector2 value)
    {
        // OnMoveHold?.Invoke(CameraRelativeDirection.AlignToCamera(value));
    }

    public override void RemoveInputs(Controller controller)
    {
        throw new System.NotImplementedException();
    }

    public override void SetInputs(Controller controller)
    {
        throw new System.NotImplementedException();
    }
}
