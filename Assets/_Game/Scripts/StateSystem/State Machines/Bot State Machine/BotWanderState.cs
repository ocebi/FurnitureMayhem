using System.Collections;
using System.Collections.Generic;
using AISystem;
using StateSystem;
using UnityEngine;

public class BotWanderState : State
{
    Bot bot;
    Transform chaseTarget;
    float randomWaitTime;
    private Vector3? destination;
    private float walkPointStartTime;

    public override void Initialize(StateMachine i_StateMachine, GameObject i_Owner)
    {
        base.Initialize(i_StateMachine, i_Owner);
        bot = i_Owner.GetComponentInParent<Bot>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        TryFindRandomDestination();
    }

    public override void OnStateUpdate()
    {
        TryFindRandomDestination();

        if (Time.time - walkPointStartTime >= 5f)
        {
            RemoveDestination();
            StateMachine.SetNewState(StateMachine.GetDefaultState());
        }
        else if (Time.time - StateStartTime >= 2.25f) //check values in the idle state
        {
            StateMachine.SetNewState(StateMachine.GetDefaultState());
        }
        else if (destination.HasValue && Vector3.Distance(transform.position, destination.Value) <= 1f) //reached destination
        {
            RemoveDestination();
            StateMachine.SetNewState(StateMachine.GetDefaultState());
        }
    }

    private void TryFindRandomDestination()
    {
        if (!destination.HasValue)
        {
            SearchWalkPoint();
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = UnityEngine.Random.Range(-bot.SightRange, bot.SightRange);
        float randomX = UnityEngine.Random.Range(-bot.SightRange, bot.SightRange);
        destination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (bot.AIMover.IsValidLocation(destination.Value))
        {
            walkPointStartTime = Time.time;
            // bot.MoveToPosition(destination.Value);
            bot.AIMover.MoveToTarget(destination.Value);
        }
        else
        {
            destination = null;
        }
    }

    private void RemoveDestination()
    {
        destination = null;
        bot.ReleaseMove();
    }
}