using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessManager : Singleton<PostProcessManager>
{
    [SerializeField]
    private Volume m_Volume;

    private Beautify.Universal.Beautify m_Beautify;

    protected override void OnAwakeEvent()
    {
        base.OnAwakeEvent();
        if (m_Volume.profile.TryGet(out m_Beautify))
        {
            Debug.LogError("Beautify profile set");
        }
    }

    [Button]
    public void SetSaturation(bool i_Enabled, float i_Value = 0)
    {
        m_Beautify.saturate.overrideState = i_Enabled;
        m_Beautify.saturate.value = i_Value;
    }
}