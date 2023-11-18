#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using Sirenix.OdinInspector;
using StateSystem;
using UnityEngine;

namespace AISystem
{
    public class Bot : AI
    {
        [ReadOnly]
        public StateMachine StateMachine;
        public float SightRange = 18f;
        public float AttackRange = 10f;
        public LayerMask TargetMask;
        [SerializeField, ReadOnly]
        private AttackController m_AttackController;

        public bool CanAttack => m_AttackController.IsCooldownFinished;

        [Button]
        private void setRefs()
        {
            StateMachine = transform.FindDeepChild<StateMachine>("BotStateMachine");
            m_AttackController = GetComponent<AttackController>();
        }

        private void Start()
        {
            Initialize();
        }

        [Button("Initialize AI")]
        public override void Initialize()
        {
            base.Initialize();
            StateMachine.ActivateStateMachine();
        }

        [Button]
        public override void ResetAI()
        {
            StateMachine.ResetStateMachine();
            ReleaseMove();
        }

        private void OnDisable()
        {
            ResetAI();
        }

        protected virtual void OnDestroy()
        {
            RemoveSensors();
        }

        public void Attack(Transform i_AttackTarget)
        {
            if (i_AttackTarget.TryGetComponent<Health>(out var health))
            {
                m_AttackController.Attack((i_AttackTarget.position - transform.position).normalized);
                Debug.LogError("Attack");
            }
            else
            {
                Debug.LogError("Health does not exist on attack target");
            }
        }

        public override void MoveToPosition(Vector3 position)
        {
            InputController?.SetMoveInput(CalculateInputDirection(position));
            // InputController?.SetMoveInput(position);
            // InputController.SetMoveInput(position);

        }

        public override void ReleaseMove()
        {
            // InputController?.SetMoveInput(Vector2.zero);
            InputController?.SetMoveInput(Vector2.zero);
        }

        private Vector2 CalculateInputDirection(Vector3 position) //position to input conversion logic
        {
            Vector3 direction = (position - transform.position).normalized;
            return new Vector2(direction.x, direction.z);
        }
        
        #if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected() 
        {
            Handles.color = Color.blue;
            Handles.DrawWireArc(transform.position, Vector3.up, transform.right, 360, SightRange);
        }
        #endif

    }
}