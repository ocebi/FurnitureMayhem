// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Unity.Collections;
// using UnityEngine;
//
// namespace AISystem
// {
//     public class BotStateMachine : MonoBehaviour
//     {
//         private Dictionary<Type, BotState> availableStates;
//         public BotState CurrentState { get; private set; }
//         //public event Action<BotState> OnStateChanged;
//         public bool availableStatesInitialized = false;
// #if UNITY_EDITOR
//         [ReadOnly]
//         public string CurrentStateName;
// #endif
//
//
//         private void Update()
//         {
//             if (!availableStatesInitialized) return;
//             if (CurrentState == null)
//             {
//                 SetNewState(availableStates.First().Key);
//                 //CurrentState = availableStates.First().Value;
//                 //SetNewState(typeof(IdleState));
//             }
//
//             CurrentState.OnStateUpdate();
//
//             var newState = CurrentState?.Tick();
//             if (newState != null &&
//                 newState != CurrentState?.GetType())
//             {
//                 SetNewState(newState);
//             }
//
// #if UNITY_EDITOR
//             CurrentStateName = CurrentState.ToString();
// #endif
//         }
//
//         public void SetStates(Dictionary<Type, BotState> states)
//         {
//             //_availableStates = states;
//             availableStates = new Dictionary<Type, BotState>();
//             foreach (var state in states)
//             {
//                 availableStates.Add(state.Key, state.Value);
//             }
//             availableStatesInitialized = true;
//             Debug.Log("Available states set.");
//         }
//
//         public void SetNewState(Type newState)
//         {
//             if (CurrentState != null)
//             {
//                 CurrentState.OnStateExit();
//             }
//             if(availableStates.ContainsKey(newState))
//             {
//                 CurrentState = availableStates[newState];
//                 CurrentState.OnStateEnter();
//             }
//             else
//                 Debug.LogError("State does not exist in the dictionary");
//
//             //OnStateChanged?.Invoke(CurrentState);
//         }
//
//         public Type GetDefaultState()
//         {
//             return availableStates.First().Key;
//         }
//     }
// }