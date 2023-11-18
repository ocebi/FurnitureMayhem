using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeatroy : MonoBehaviour
{
    [SerializeField] float deathTime = 3;

    void Start()
    {
        Destroy(gameObject, deathTime);
    }

}
