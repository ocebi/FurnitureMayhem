using UnityEngine;

public class Health : Replenishable
{
    public bool IsInvulnerable;

    public override void SetInitialValue(int i_Value)
    {
        base.SetInitialValue(i_Value);
        IsInvulnerable = false;
    }

    protected override void SetNewValue(int i_Value)
    {
        base.SetNewValue(i_Value);
        if (CurrentValue == 0)
            Destroy(gameObject);
    }
}