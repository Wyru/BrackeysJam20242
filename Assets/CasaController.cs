using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasaController : MonoBehaviour
{
    [SerializeField] private List<GameObject> figurines; 
    void Start()
    {
        if(GameManager.instance.figurinesFounded.Count > 0)
        {
            foreach (string foundObjectName in GameManager.instance.figurinesFounded)
            {
                // Check if any GameObject in allObjects has a matching name
                foreach (GameObject obj in figurines)
                {
                    if (obj.name == foundObjectName)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
    }
}
