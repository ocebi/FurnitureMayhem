using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig/Create GameConfig", fileName = "GameConfig")]
public class GameConfig : SingletonScriptableObject<GameConfig>
{
    [Header("Camera")] 
    public float RegularFollowSpeed = 100;
    public float SoulTransitionFollowSpeed = 10;
    public List<GameObject> RobotPrefabs = new List<GameObject>();
}