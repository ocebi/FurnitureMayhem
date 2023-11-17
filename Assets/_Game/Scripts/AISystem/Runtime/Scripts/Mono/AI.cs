using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AISystem
{
    public class AI : MonoBehaviour
    {
        [ReadOnly]
        public AgentController AgentController;
        [SerializeField, ReadOnly]
        protected AgentInputController InputController;
        [ReadOnly]    
        public Sensor AISensor;
        [ReadOnly]
        public AIMover AIMover;

        public virtual void Initialize()
        {
            if (AgentController == null)
                AgentController = GetComponent<AgentController>();
            if (InputController == null)
                InputController = GetComponent<AgentInputController>();
            AddSensors();
        }

        public virtual void ResetAI()
        {
            
        }

        public virtual void MoveToPosition(Vector3 position)
        {
        }

        public virtual void ReleaseMove()
        {
        }
        
        protected virtual void AddSensors()
        {
            if (AISensor == null)
                AISensor = gameObject.AddComponent<Sensor>();
            if (AIMover == null)
                AIMover = gameObject.AddComponent<AIMover>();
        }
        
        protected virtual void RemoveSensors()
        {
            Destroy(AISensor);
            Destroy(AIMover);
        }
    }
}