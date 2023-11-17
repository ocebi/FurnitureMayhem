using StateSystem;
using UnityEngine;

public class SpecialVisionState : State
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Time.timeScale = 0.25f;
        //TODO: Commented out
        // PostProcessManager.Instance.SetSaturation(true, -1.5f);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Time.timeScale = 1f;
        //TODO: Commented out
        // PostProcessManager.Instance.SetSaturation(false);
    }
}