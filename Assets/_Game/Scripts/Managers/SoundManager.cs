using Sirenix.OdinInspector;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, ReadOnly] 
    private AudioSource[] m_AudioSources;

    private bool m_IsSoundEnabled;
    private int m_AudioSourceCounter;
    
    [Button]
    private void SetRefs()
    {
        m_AudioSources = GetComponentsInChildren<AudioSource>(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_IsSoundEnabled = true;
    }

    private void OnValidate()
    {
        SetRefs();
    }

    public void PlaySound(eSoundType eSoundType)
    {
        if (!GameConfig.Instance.SoundDictionary.ContainsKey(eSoundType))
            return;
        if (!m_IsSoundEnabled) 
            return;
        // m_IsSoundEnabled = false;
        // Invoke(nameof(EnableSound), Random.Range(GameConfig.Instance.PlayThreshold.x, GameConfig.Instance.PlayThreshold.y));
        var currentAudioSource = m_AudioSources[m_AudioSourceCounter % m_AudioSources.Length];
        m_AudioSourceCounter++;
        currentAudioSource.pitch = Random.Range(GameConfig.Instance.SoundPitchRange.x, GameConfig.Instance.SoundPitchRange.y);
        currentAudioSource.PlayOneShot(GameConfig.Instance.SoundDictionary[eSoundType]);
    }
    
    private void EnableSound()
    {
        m_IsSoundEnabled = true;
    }
}