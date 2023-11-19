using UnityEngine;

public class Health : Replenishable
{
    public bool IsInvulnerable;
    public override void SetInitialValue(int i_Value)
    {
        base.SetInitialValue(i_Value);
        IsInvulnerable = false;
    }

    public bool WillHealthDeplete(int decreaseAmount)
    {
        return CurrentValue - decreaseAmount <= 0;
    }
}