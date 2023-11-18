using StateSystem;
using UnityEngine;

public class SpecialVisionState : State
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Time.timeScale = 0.25f;
        PostProcessManager.Instance.SetSaturation(true, -1.5f);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Time.timeScale = 1f;
        PostProcessManager.Instance.SetSaturation(false);
    }
}