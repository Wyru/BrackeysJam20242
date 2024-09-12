using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultCanvasBehavior : MonoBehaviour
{

  public DefaultCanvasBehavior instance;

  public RawImage hand;
  public TextMeshProUGUI itemHoldDescription;
  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Debug.LogWarning("Multiplas instâncias do canvas default!");
      DestroyImmediate(gameObject);
      return;
    }

    DontDestroyOnLoad(this);
  }

}
