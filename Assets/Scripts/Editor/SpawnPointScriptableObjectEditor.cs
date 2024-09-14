using UnityEditor;
using System.Linq;
using System.Collections.Generic;
[CustomEditor(typeof(SpawnPointScriptableObject))]
public class SpawnPointScriptableObjectEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    var spawnPoint = target as SpawnPointScriptableObject;

    string[] sceneNames = EditorBuildSettings.scenes
                                .Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path))
                                .ToArray();


    for (int i = 0; i < sceneNames.Length; i++)
    {
      if (sceneNames[i] == spawnPoint.targetSceneName)
      {
        spawnPoint.targetSceneId = i;
        break;
      }
    }
    // Display a dropdown to select the scene
    int newSelectedScene = EditorGUILayout.Popup("Scene", spawnPoint.targetSceneId, sceneNames);


    if (newSelectedScene != spawnPoint.targetSceneId)
    {
      Undo.RecordObject(spawnPoint, "Changed Scene Selection");
      spawnPoint.targetSceneId = newSelectedScene;
      spawnPoint.targetSceneName = sceneNames[newSelectedScene];
      EditorUtility.SetDirty(spawnPoint);
    }
  }
}