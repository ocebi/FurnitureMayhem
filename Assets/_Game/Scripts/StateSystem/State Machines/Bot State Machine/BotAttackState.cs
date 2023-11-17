using System.Collections;
using System.Collections.Generic;
using AISystem;
using StateSystem;
using UnityEngine;

public class BotAttackState : State
{
    Bot bot;
    Transform attackTarget;
    float waitTime = 0.5f;

    public override void Initialize(StateMachine i_StateMachine, GameObject i_Owner)
    {
        base.Initialize(i_StateMachine, i_Owner);
        bot = i_Owner.GetComponentInParent<Bot>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        //Check sensors
        attackTarget = null;
        bot.AIMover.StopMoving();
    }

    public override void OnStateUpdate()
    {
        attackTarget = bot.AISensor.IsObjectInRange(bot.AttackRange, bot.TargetMask);
        if (attackTarget != null)
        {
            bot.Attack(attackTarget);
        }
        if (Time.time - StateStartTime > waitTime)
        {
            StateMachine.SetNewState(StateMachine.GetDefaultState());
        }
    }
}