using System;
using UnityEngine;

namespace AgentAttributeSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "AgentAttributeModifierWithList", menuName = "AgentAttributes/ModifierWithList")]
    public class AgentAttributeModifierWithList : AgentAttributeModifier
    {
        [SerializeField] AgentAttributes[] modifierValues;
        public AgentAttributes GetModifierValues(int level)
        {
            return modifierValues[level-1];
        }
    }

}