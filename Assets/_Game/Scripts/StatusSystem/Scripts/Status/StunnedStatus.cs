using System.Collections;
using System.Collections.Generic;
using AgentSystem;
using AISystem;
using StateSystem;
using UnityEngine;

namespace StatusSystem
{

    [CreateAssetMenu(fileName = "StunnedStatus", menuName = "StatusSystem/Stunned Status")]
    public class StunnedStatus : Status
    {
        private AgentController agentController;
        StateMachine botStateMachine;
        [SerializeField] private GameObject stunParticlePrefab;
        private GameObject stunParticle;

        public override void ActivateStatus()
        {
             Vector3 stunParticlePosition = Owner.transform.position;
            stunParticlePosition.y = 0.1f;
            stunParticle = Instantiate(stunParticlePrefab, stunParticlePosition, Quaternion.Euler(90, 0, 0));
            stunParticle.transform.parent = Owner.transform;
            StunAgent();
        }

        public override void ReActivateStatus()
        {
            Vector3 stunParticlePosition = Owner.transform.position;
            stunParticlePosition.y = 0.1f;
            stunParticle = Instantiate(stunParticlePrefab, stunParticlePosition,  Quaternion.Euler(90, 0, 0));
            stunParticle.transform.parent = Owner.transform;
            StunAgent();
        }

        public override void DeActivateStatus()
        {
            if (EffectCount <= 1)
                ReleaseAgent();
        }

        public override void Initialize(StatusController owner)
        {
            base.Initialize(owner);
            agentController = owner.GetComponent<AgentController>();
            botStateMachine = owner.GetComponent<StateMachine>();

        }

        public void StunAgent()
        {
            if (botStateMachine)
                botStateMachine.enabled = false;
            //TODO: Not implemented
            // agentController.DisableMovement();
        }

        public void ReleaseAgent()
        {
            if (botStateMachine)
                botStateMachine.enabled = true;
            //TODO: Not implemented
            // agentController.EnableMovement();
        }
    }
}