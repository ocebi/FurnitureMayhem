using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private Rigidbody m_Rigidbody;
    private AgentController m_Owner;

    private void SetRefs()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        SetRefs();
    }

    public void Shoot(Vector3 direction, AgentController owner)
    {
        m_Owner = owner;
        m_Rigidbody.AddForce(direction * 20f, ForceMode.Impulse);
        Invoke(nameof(DestroySelf), 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_Owner == null)
            return;
        if (other.TryGetComponent<AgentController>(out var agentController) &&
            agentController != m_Owner &&
            agentController.IsHacked != m_Owner.IsHacked)
        {
            var health = agentController.GetComponent<Health>(); 
            if (health.WillHealthDeplete(10) && m_Owner && m_Owner.HasSoul)
                GameStateManager.Instance.OnAgentHacked();
            health.DecreaseValue(10);
            CancelInvoke();
            Destroy(gameObject);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}