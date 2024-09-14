using UnityEngine;

public class ItemsController : MonoBehaviour
{
    public int price;
    public int heathRecover;
    public bool alreadyPurchased;

    void Awake()
    {
        alreadyPurchased = false;
    }
}
