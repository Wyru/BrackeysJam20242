using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultCanvasBehavior : MonoBehaviour
{

  public static DefaultCanvasBehavior instance;

  public RawImage hand;
  public TextMeshProUGUI itemHoldDescription;
  private void Awake()
  {
    if (instance != null)
    {
      Debug.LogWarning("Multiplas instâncias do canvas default!");
      DestroyImmediate(gameObject);
      return;
    }

    instance = this;
    DontDestroyOnLoad(this);
  }

}
