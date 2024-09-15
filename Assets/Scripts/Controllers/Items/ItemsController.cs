using UnityEngine;

public class ItemsController : MonoBehaviour
{
  public int price;
  public int heathRecover;
  public bool alreadyPurchased = false;
  public bool usable = true;
  public bool isGuarana = false;
  public bool CanBeUsed
  {
    get { return alreadyPurchased && usable; }
  }

  void Awake()
  {
    // alreadyPurchased = false;
  }

  public void UseItem()
  {
    Debug.Log($"Usou {name}");
    GameManager.instance.SetHealth(heathRecover);
  }
}
