using AISystem;
using StateSystem;
using UnityEngine;


public class BotIdleState : State
{
    Bot bot;
    Transform chaseTarget;
    float randomWaitTime;

    public override void Initialize(StateMachine i_StateMachine, GameObject i_Owner)
    {
        base.Initialize(i_StateMachine, i_Owner);
        bot = i_Owner.GetComponentInParent<Bot>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        //Check sensors
        chaseTarget = null;
        chaseTarget = bot.AISensor.IsObjectInRange(bot.SightRange, bot.TargetMask);

        if (chaseTarget != null && bot.AIMover.IsValidLocation(chaseTarget.transform.position))
        {
            StateMachine.SetNewState(nameof(BotPursueState));
        }
        else
        {
            int backToWanderState = UnityEngine.Random.Range(0, 10);
            if (backToWanderState < 8)
            {
                StateMachine.SetNewState(nameof(BotWanderState));
            }
            else
            {
                randomWaitTime = Random.Range(0, 1f);
                bot.ReleaseMove();
            }
        }
    }

    public override void OnStateUpdate()
    {
        if (Time.time - StateStartTime > randomWaitTime)
        {
            StateMachine.SetNewState(StateMachine.GetDefaultState());
        }
    }
}