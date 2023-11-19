using System;
using System.Collections.Generic;
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
}

[Serializable]
public class CollectableDataDict : ExtensionMethods.UnitySerializedDictionary<eCollectable, CollectableData> { }

[Serializable]
public class CollectableData
{
    public Sprite Sprite;
}