using System;
using Sirenix.OdinInspector;
using UnityEngine;
public class TransformFollower : MonoBehaviour
{
    public Action OnTargetReached;
    public bool FollowX;
    public bool FollowY;
    public bool FollowZ;
    [SerializeField] protected Transform FollowTarget;
    [SerializeField] float m_Speed = 5;
    
    private bool m_CallTargetReachAction;

    protected virtual void Update() 
    {
        if(FollowTarget == null) return;

        transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(FollowX ? FollowTarget.position.x : transform.position.x, 
                FollowY ? FollowTarget.position.y : transform.position.y,
                FollowZ ? FollowTarget.position.z : transform.position.z), 
            m_Speed * Time.deltaTime);
        if (m_CallTargetReachAction &&
            Vector3.Distance(transform.position, FollowTarget.position) < 0.5f)
        {
            OnTargetReached.InvokeSafe();
            m_CallTargetReachAction = false;
        }
    }

    public void SetFollowTarget(Transform target)
    {
        FollowTarget = target;
        m_CallTargetReachAction = true;
    }

    public void SetSpeed(float i_Speed)
    {
        m_Speed = i_Speed;
    }
    
    [Button]
    private void setPosition()
    {
        if (FollowTarget)
            transform.position = FollowTarget.position;
    }
}