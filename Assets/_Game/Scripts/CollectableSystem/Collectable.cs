using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public eCollectable CollectableType => m_CollectableType;
    
    [SerializeField]
    private eCollectable m_CollectableType;

    [SerializeField, ReadOnly] 
    private GameObject m_Model;

    private bool m_IsCollected;

    private void SetRefs()
    {
        m_Model = transform.FindDeepChild<GameObject>("Model");
    }

    private void OnValidate()
    {
        SetRefs();
    }

    public void Collect()
    {
        if (m_IsCollected)
            return;
        m_Model.SetActive(false);
        Invoke(nameof(Respawn), 10f);
        // Destroy(gameObject);
    }

    private void Respawn()
    {
        m_Model.SetActive(true);
        m_IsCollected = false;
    }
}