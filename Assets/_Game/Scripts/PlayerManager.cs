using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public AgentController ActiveAgent => m_AttachedAgentController;
    [SerializeField, ReadOnly]
    private AgentController m_AttachedAgentController;
    [SerializeField, ReadOnly]
    private TransformFollower m_TransformFollower;
    [SerializeField]
    private VisualEffect m_Vfx;
    [SerializeField]
    private MeshFilter m_MeshFilter;

    [Button]
    private void setRefs()
    {
        m_TransformFollower = GetComponent<TransformFollower>();
    }
    
    private void OnEnable()
    {
        m_TransformFollower.OnTargetReached += onSoulTargetReached;
    }

    private void OnDisable()
    {
        m_TransformFollower.OnTargetReached -= onSoulTargetReached;
    }

    [Button]
    public void SetPlayerTarget(AgentController i_AgentController)
    {
        if (m_AttachedAgentController != null)
            m_AttachedAgentController.SetLocalControl(false);
        m_AttachedAgentController = i_AgentController;
        // GameStateManager.Instance.StateMachine.SetNewState(nameof(SoulJumpState));
        m_TransformFollower.SetSpeed(GameConfig.Instance.SoulTransitionFollowSpeed);
        CameraManager.Instance.SetCameraTarget(i_AgentController.VisualTransform);
        // m_TransformFollower.SetFollowTarget(i_AgentController.VisualTransform);
        setVfxOnMove();
        GameStateManager.Instance.SetLastJumpTime();
    }

    private void onSoulTargetReached()
    {
        if (m_AttachedAgentController == null)
            return;

        m_AttachedAgentController.SetLocalControl(true);
        m_TransformFollower.SetSpeed(GameConfig.Instance.RegularFollowSpeed);
        if (GameStateManager.Instance.StateMachine.CurrentState?.GetType() == typeof(PlayerTransitionState))
            GameStateManager.Instance.StateMachine.SetNewState(nameof(GameplayState));

        //TODO get mesh or skinned mesh renderer, send it to right method
        setVfxReached(m_AttachedAgentController.VisualTransform.GetComponentInChildren<SkinnedMeshRenderer>());
    }

    private void setVfxOnMove()
    {
        // m_Vfx.SetMesh("Mesh", m_MeshFilter.mesh);
        // m_Vfx.SetBool("IsMesh", true);
        // m_Vfx.SetFloat("follow force", 5);
        // m_Vfx.SetFloat("Turbulant", 4);
    }

    private void setVfxReached(Mesh i_mesh)
    {
        // m_Vfx.SetMesh("Mesh", i_mesh);
        // m_Vfx.SetBool("IsMesh", true);
        // setVfxReachedValues();
    }

    private void setVfxReached(SkinnedMeshRenderer i_meshRenderer)
    {
        // Debug.Log("workin");
        // m_Vfx.SetSkinnedMeshRenderer("SkinnedMeshRenderer", i_meshRenderer);
        // //m_Vfx.SetMesh("Mesh", null);
        // m_Vfx.SetBool("IsMesh", false);
        // setVfxReachedValues();
    }

    private void setVfxReachedValues() 
    {
        // m_Vfx.SetFloat("follow force", 0);
        // m_Vfx.SetFloat("Turbulant", 0);
    }
}
