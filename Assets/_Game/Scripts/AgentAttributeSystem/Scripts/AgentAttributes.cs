using System;
using UnityEngine;

namespace AgentAttributeSystem
{
    [Serializable]
    public class AgentAttributes
    {
        //[ReadOnly]
        //public int TotalPower = 0;
        [Range(0.1f, 10000)]
        public float Health = 1;
        [Range(0.1f, 100)]
        public float VisionMultiplier = 1;
        //[ReadOnly]
        //public float CurrentDamage;
        [Range(0.1f, 1000)]
        public float DamageMultipler = 1;
        //[ReadOnly]
        //public float CurrentAttackSpeed;
        [Range(.1f, 20)]
        public float AttackSpeedMultiplier = 1;
        //[ReadOnly]
        //public float CurrentAttackRange;
        [Range(0.1f, 20)]
        public float AttackRangeMultiplier = 1;
        [Range(0.1f, 100)]
        public float MoveSpeed = 1;
    
        public AgentAttributes GetModifiedAgentAttribute(AgentAttributes modifier)
        {
            var newAgentAttributes = new AgentAttributes();
            newAgentAttributes.Health = Health * modifier.Health;
            newAgentAttributes.VisionMultiplier = VisionMultiplier * modifier.VisionMultiplier;
            newAgentAttributes.DamageMultipler = DamageMultipler * modifier.DamageMultipler;
            newAgentAttributes.AttackSpeedMultiplier = AttackSpeedMultiplier * modifier.AttackSpeedMultiplier;
            newAgentAttributes.AttackRangeMultiplier = AttackRangeMultiplier * modifier.AttackRangeMultiplier;
            newAgentAttributes.MoveSpeed = MoveSpeed * modifier.MoveSpeed;
            return newAgentAttributes;
        }


    }
}