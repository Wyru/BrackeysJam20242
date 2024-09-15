using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;


public class ReScalerPrefab : MonoBehaviour
{
     GameObject[] gameObjects;


    private void Awake()
    {
        gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in gameObjects){
            if(go.tag == "CanPickUp"){
                GameObject papai = new GameObject(go.name);
                go.transform.position = papai.transform.position;
                go.transform.parent = papai.transform;
                string path = "Assets/Prefabs/Comidas/Good Food";
                path = AssetDatabase.GenerateUniqueAssetPath(path);
                PrefabUtility.SaveAsPrefabAssetAndConnect(papai, path, InteractionMode.UserAction);
            }
        }
    
    }
}
