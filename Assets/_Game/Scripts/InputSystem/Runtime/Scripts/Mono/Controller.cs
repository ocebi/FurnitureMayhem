using AgentSystem;
using InputSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [ReadOnly]
    public AgentController AgentController;
    protected InputListener inputListener;

    public virtual void Initialize()
    {
        AgentController = GetComponent<AgentController>();
    }
}