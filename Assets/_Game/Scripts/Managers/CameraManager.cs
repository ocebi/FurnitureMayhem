using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Camera Camera => m_Camera;
    private Camera m_Camera;
    [SerializeField, ReadOnly]
    private eCameras m_ActiveCamera;
    
    // [SerializeField, ReadOnly]
    // private CinemachineTargetGroup m_TargetGroup;

    [SerializeField, ReadOnly]
    private CinemachineVirtualCamera m_GameplayCamera;

    [SerializeField, ReadOnly]
    private TransformFollower m_CameraFollowTarget;

    [Button]
    private void setRefs()
    {
        m_CameraFollowTarget = FindObjectOfType<PlayerManager>().GetComponent<TransformFollower>();
        m_GameplayCamera = transform.FindDeepChild<CinemachineVirtualCamera>("GameplayCamera");
    }
    
    protected override void OnAwakeEvent()
    {
        m_Camera = Camera.main;
        m_CameraFollowTarget.FollowX = true;
        m_CameraFollowTarget.FollowY = true;
        m_CameraFollowTarget.FollowZ = true;
        m_CameraFollowTarget.SetSpeed(GameConfig.Instance.RegularFollowSpeed);
        SetCamera(eCameras.Gameplay); //TODO: Do this in GameplayState
    }

    protected override void OnEnable()
    {
        GameStateManager.Instance.StateMachine.OnStateChanged += onStateChanged;
    }

    public override void OnDisable()
    {
        if (!GameStateManager.IsInstanceNull) //TODO: To refactor
            GameStateManager.Instance.StateMachine.OnStateChanged -= onStateChanged;
    }

    public void SetCamera(eCameras i_Camera)
    {
        m_ActiveCamera = i_Camera;
        if (i_Camera == eCameras.Gameplay)
        {
            m_GameplayCamera.gameObject.SetActive(true);
        }
    }

    public void SetCameraTarget(Transform i_Transform)
    {
        // if (m_CurrentCameraTarget != null)
        //     m_TargetGroup.RemoveMember(m_CurrentCameraTarget);
        // m_TargetGroup.AddMember(i_Transform, 1, 1);
        m_CameraFollowTarget.SetFollowTarget(i_Transform);
    }

    private void onStateChanged(string i_StateName)
    {
        // if (i_StateName == nameof(GameplayState))
        //     m_CameraFollowTarget.SetSpeed(GameConfig.Instance.RegularFollowSpeed);
        // else if (i_StateName == nameof(SpecialVisionState))
        //     m_CameraFollowTarget.SetSpeed(GameConfig.Instance.SoulTransitionFollowSpeed);
    }
}
