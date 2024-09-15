using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlick : MonoBehaviour
{
    public Light lightSource;
    public GameObject obj;
    private bool status = false;
    private float t = 0;
    private float randomLimit = .2f;

    void LateUpdate()
    {
        if (status)
        {
            if (t > randomLimit)
            {
                if (Random.Range(0, 100) < 50)
                {
                    lightSource.enabled = false;
                    obj.SetActive(false);
                    status = false;
                    t = 0f;
                    randomLimit = Random.Range(0.2f, 0.7f);
                }
            }
        }
        else
        {
            if (t > 0.2f)
            {
                if (Random.Range(0, 100) < 50)
                {
                    lightSource.enabled = true;
                    obj.SetActive(true);
                    status = true;
                    t = 0f;
                    randomLimit = Random.Range(0.2f, 0.7f);
                }
            }
        }


        t += Time.deltaTime;
    }
}
