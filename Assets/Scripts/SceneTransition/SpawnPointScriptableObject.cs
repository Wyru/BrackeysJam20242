using UnityEngine;



[CreateAssetMenu(fileName = "SpawnPointScriptableObject", menuName = "SpawnPointScriptableObject", order = 0)]
public class SpawnPointScriptableObject : ScriptableObject
{
  [HideInInspector]
  public int targetSceneId;
  [HideInInspector]
  public string targetSceneName;
}