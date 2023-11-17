// using AgentAttributeSystem;
// using Sirenix.OdinInspector;
// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace AgentSystem
// {
//     public class Agent : MonoBehaviour
//     {
//         public int CharacterID = 1;
//         public string CharacterName = "";
//         //public CharacterInfoData CharacterInfoData;
//         [SerializeField]
//         AgentAttributes baseAgentAttributes;
//         [ReadOnly] [SerializeField] AgentAttributes currentAgentAttributes;
//         public AgentAttributes CurrentAgentAttributes { get { if (currentAgentAttributes == null) RecalculateAgentAttributes(); return currentAgentAttributes; } private set { currentAgentAttributes = value; } }
//         //AgentAttributeModifierWithList modifierPerLevel;
//         [SerializeField]
//         List<AgentAttributeModifier> agentAttributeModifiers = new List<AgentAttributeModifier>();
//         public int TeamId = 0;
//         public virtual bool IsAgentAlive { get { throw new NotImplementedException(); } }
//
//
//         public static Action<Agent> OnAgentCreated;
//         public Action<float> OnAgentBaseHealthChanged;
//         public Action<float> OnAgentVisionChanged;
//         public Action<float> OnAgentMoveSpeedChanged;
//         public Action<float> OnAgentDamageChanged;
//         public Action<float> OnAgentAttackSpeedChanged;
//         public Action<float> OnAgentAttackRangeChanged;
//
//         protected virtual void Awake()
//         {
//             CurrentAgentAttributes = baseAgentAttributes;
//             //gameObject.AddComponent<AgentInitializer>();
//             RecalculateAgentAttributes();
//         }
//
//         private void Start()
//         {
//             OnAgentCreated?.Invoke(this);
//             SendCurrentAgentAttributes();
//         }
//
//
//         public void SendCurrentAgentAttributes()
//         {
//             OnAgentBaseHealthChanged?.Invoke(CurrentAgentAttributes.Health);
//             //OnAgentDamageChanged?.Invoke(CurrentAgentAttributes.CurrentDamage);
//             //OnAgentAttackSpeedChanged?.Invoke(CurrentAgentAttributes.CurrentAttackSpeed);
//             //OnAgentAttackRangeChanged?.Invoke(CurrentAgentAttributes.CurrentAttackRange);
//             OnAgentMoveSpeedChanged?.Invoke(CurrentAgentAttributes.MoveSpeed);
//         }
//
//         //public void SetRandomAgentInfo(int minLevel, int maxLevel)
//         //{
//         //    CharacterInfoData = CharacterInfoManager.GetRandomCharacterInfo(minLevel, maxLevel);
//         //    //Debug.Log(CharacterInfoData.Level, gameObject);
//         //    //Debug.Log(CharacterInfoData.Experience, gameObject);
//         //    //Debug.Log(CharacterInfoData.Level, gameObject);
//         //    //Debug.Log(CharacterInfoData.Experience, gameObject);
//         //    RecalculateAgentAttributes();
//         //}
//
//         [Button("Initialize Agent")]
//         public virtual void InitializeAgent()
//         {
//             gameObject.AddComponent<AgentController>();
//             gameObject.AddComponent<PlayerInputController>();
//         }
//
//         public void SetBaseRobotAttributes()
//         {
//             OnAgentBaseHealthChanged?.Invoke(CurrentAgentAttributes.Health);
//             OnAgentMoveSpeedChanged?.Invoke(CurrentAgentAttributes.MoveSpeed);
//         }
//
//         [ContextMenu("Recalculate")]
//         public void RecalculateAgentAttributes()
//         {
//             var oldAgentAttributes = CurrentAgentAttributes;
//             if (oldAgentAttributes == null)
//                 oldAgentAttributes = baseAgentAttributes;
//             CurrentAgentAttributes = baseAgentAttributes;
//             //for (int i = 1; i < CharacterInfoData.Level; i++)
//             //{
//             //    CurrentAgentAttributes = CurrentAgentAttributes.GetModifiedAgentAttribute(modifierPerLevel.GetModifierValues(i));
//             //}
//             foreach (var modifier in agentAttributeModifiers)
//             {
//                 CurrentAgentAttributes = CurrentAgentAttributes.GetModifiedAgentAttribute(modifier.ModifierValues);
//             }
//
//             if (oldAgentAttributes.Health != CurrentAgentAttributes.Health)
//                 OnAgentBaseHealthChanged?.Invoke(CurrentAgentAttributes.Health);
//             if (oldAgentAttributes.MoveSpeed != CurrentAgentAttributes.MoveSpeed)
//                 OnAgentMoveSpeedChanged?.Invoke(CurrentAgentAttributes.MoveSpeed);
//         }
//
//         public void AddModifier(AgentAttributeModifier modifier)
//         {
//             Debug.Log("Agent AddModifier");
//             agentAttributeModifiers.Add(modifier);
//             RecalculateAgentAttributes();
//         }
//
//         public void RemoveModifier(AgentAttributeModifier modifier)
//         {
//             agentAttributeModifiers.Remove(modifier);
//             RecalculateAgentAttributes();
//         }
//     }
// }