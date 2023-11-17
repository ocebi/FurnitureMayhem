using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public bool IsCooldownFinished => Time.time - m_LastAttackTime >= m_Cooldown;
    [SerializeField] 
    private eAttackType m_AttackType;
    [SerializeField] 
    private float m_Cooldown;
    private float m_LastAttackTime;

    [SerializeField, ReadOnly]
    private AgentController m_AgentController;

    [SerializeField] 
    private Projectile m_ProjectilePrefab;

    private void SetRefs()
    {
        m_AgentController = GetComponent<AgentController>();
    }

    private void OnValidate()
    {
        SetRefs();
    }

    public void Attack(Vector3 direction)
    {
        if (Time.time - m_LastAttackTime < m_Cooldown)
            return;
        if (m_AttackType == eAttackType.Melee)
        {
            var colliders = Physics.OverlapBox(transform.position + direction, Vector3.one, Quaternion.identity, LayerMask.GetMask("Agent"));
            AgentController chosenAgent = null;
            if (colliders.Length == 0)
                return;
            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<AgentController>(out var agentController) && 
                    agentController != m_AgentController &&  
                    !agentController.IsHacked)
                {
                    chosenAgent = agentController;
                    break;
                }
            }

            if (chosenAgent && chosenAgent.TryGetComponent<Health>(out var health))
            {
                health.DecreaseValue(10);
                Debug.LogError("Damaged");
            }
            //TODO: Play attack animation
        }
        else if (m_AttackType == eAttackType.Ranged)
        {
            var projectile = Instantiate(m_ProjectilePrefab, transform.position + direction, Quaternion.LookRotation(direction));
            projectile.Shoot(direction, m_AgentController);
        }
        
        m_LastAttackTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.forward * 2f, Vector3.one);
    }
}
