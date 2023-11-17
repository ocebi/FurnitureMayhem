using System;
using UnityEngine;

namespace AgentAttributeSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "AgentAttributeModifier", menuName = "AgentAttributes/Modifier")]
    public class AgentAttributeModifier : ScriptableObject
    {   
        public AgentAttributes ModifierValues;
    }


}