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
    [SerializeField] 
    private int m_Damage = 10;
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
            var colliders = Physics.OverlapBox(transform.position + direction, Vector3.one * 2, Quaternion.identity, LayerMask.GetMask("Agent"));
            AgentController chosenAgent = null;
            if (colliders.Length == 0)
                return;
            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<AgentController>(out var agentController) && 
                    agentController != m_AgentController &&  
                    agentController.IsHacked != m_AgentController.IsHacked)
                {
                    chosenAgent = agentController;
                    break;
                }
            }

            if (chosenAgent && chosenAgent.TryGetComponent<Health>(out var health))
            {
                if (health.WillHealthDeplete(m_Damage) && m_AgentController.HasSoul)
                    GameStateManager.Instance.OnAgentHacked();
                health.DecreaseValue(m_Damage);
            }
        }
        else if (m_AttackType == eAttackType.Ranged)
        {
            var projectile = Instantiate(m_ProjectilePrefab, transform.position + direction, Quaternion.LookRotation(direction));
            projectile.Shoot(direction, m_AgentController);
        }
        SoundManager.Instance.PlaySound(m_AgentController.AttackSound);
        m_LastAttackTime = Time.time;
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(transform.forward * 2f, Vector3.one);
    // }
}
