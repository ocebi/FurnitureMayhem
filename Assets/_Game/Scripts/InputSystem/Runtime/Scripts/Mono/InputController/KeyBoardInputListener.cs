using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInputListener : InputListener
{
    bool moveRelased = false;

    public override void RemoveInputs(Controller controller)
    {
        throw new System.NotImplementedException();
    }

    public override void SetInputs(Controller controller)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputVector != Vector2.zero)
        {
            moveRelased = true;
            inputVector = CameraRelativeDirection.AlignToCamera(inputVector);
            CallOnMove(inputVector);
        }
        else if (moveRelased)
        {
            CallOnMoveReleased();
            moveRelased = false;
        }
    }
}
