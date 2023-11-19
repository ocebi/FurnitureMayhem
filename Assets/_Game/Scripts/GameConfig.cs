using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig/Create GameConfig", fileName = "GameConfig")]
public class GameConfig : SingletonScriptableObject<GameConfig>
{
    [Header("Camera")] 
    public float RegularFollowSpeed = 100;
    public float SoulTransitionFollowSpeed = 10;
    public List<GameObject> RobotPrefabs = new List<GameObject>();
    public CollectableDataDict CollectableDataDict = new CollectableDataDict();
    public int TargetHackAmount = 10;
    public float JumpCooldown = 5;
    [Title("Sound Settings")] 
    public SoundDictionary SoundDictionary = new SoundDictionary();
    public Vector2 PlayThreshold;
    public Vector2 SoundPitchRange;
}

[Serializable]
public class CollectableDataDict : ExtensionMethods.UnitySerializedDictionary<eCollectable, CollectableData> { }
[Serializable]
public class SoundDictionary : ExtensionMethods.UnitySerializedDictionary<eSoundType, AudioClip> { }

[Serializable]
public class CollectableData
{
    public Sprite Sprite;
}