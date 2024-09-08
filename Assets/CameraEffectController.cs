using PSX;
using UnityEngine;

public class CameraEffectController : MonoBehaviour
{
  public GameObject normalPostProcessing;
  public GameObject insanePostProcessing;

  public Material normalSkybox;
  public Material InsaneSkybox;

  public bool debug = false;

  void Update()
  {
    if (debug)
    {
      if (Input.GetKeyDown(KeyCode.Alpha0))
      {
        SetNormalState();
      }
      if (Input.GetKeyDown(KeyCode.Alpha9))
      {
        SetInsateState();
      }
    }
  }

  public void SetNormalState()
  {
    normalPostProcessing.SetActive(true);
    insanePostProcessing.SetActive(false);
    ChangeSkybox(normalSkybox);
  }

  public void SetInsateState()
  {
    normalPostProcessing.SetActive(false);
    insanePostProcessing.SetActive(true);
    ChangeSkybox(InsaneSkybox);
  }


  void ChangeSkybox(Material skyboxMaterial)
  {
    RenderSettings.skybox = skyboxMaterial;
    DynamicGI.UpdateEnvironment();
  }
}
