using AISystem;
using StateSystem;
using UnityEngine;

public class BotPursueState : State
{
    Bot bot;
    Transform chaseTarget;
    Vector3? chaseTargetPosition;
    float randomWaitTime;
    bool path;
    bool isMoveStarted = false;

    public override void Initialize(StateMachine i_StateMachine, GameObject i_Owner)
    {
        base.Initialize(i_StateMachine, i_Owner);
        bot = i_Owner.GetComponentInParent<Bot>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        //Debug.Log("Inside PlayerPursueState enter");
        var inSightRange = bot.AISensor.IsObjectInRange(bot.SightRange, bot.TargetMask);

        chaseTarget = inSightRange;
        chaseTargetPosition = inSightRange.position;

        if (inSightRange == null || bot.AIMover.IsValidLocation(chaseTargetPosition.Value) == false)
        {
            StateMachine.SetNewState(StateMachine.GetDefaultState());
        }
    }

    public override void OnStateUpdate()
    {
        if (chaseTarget != null)
        {
            if(!isMoveStarted)
            {
                // bot.MoveToPosition(chaseTargetPosition.Value);
                bot.AIMover.MoveToTarget(chaseTargetPosition.Value);
                isMoveStarted = true;
            }
            if (Vector3.Distance(transform.position, chaseTargetPosition.Value) <= (bot.AttackRange * 0.9f))
            {
                RemoveTarget();
                StateMachine.SetNewState(nameof(BotAttackState));
                // botStateMachine.SetNewState(bot.GetDefaultState());
            }
            else if (Time.time - StateStartTime >= 0.5f)
            {
                StateMachine.SetNewState(StateMachine.GetDefaultState());
            }
        }
        else
        {
            RemoveTarget();
            StateMachine.SetNewState(StateMachine.GetDefaultState());
        }
    }

    public override void OnStateExit()
    {
        isMoveStarted = false;
    }

    void RemoveTarget()
    {
        bot.AIMover.StopMoving();
        chaseTarget = null;
        chaseTargetPosition = null;
    }
}