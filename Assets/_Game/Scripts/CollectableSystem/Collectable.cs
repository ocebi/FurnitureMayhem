using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public eCollectable CollectableType => m_CollectableType;
    
    [SerializeField]
    private eCollectable m_CollectableType;

    public void Collect()
    {
        Destroy(gameObject);
    }
}