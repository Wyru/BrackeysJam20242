using UnityEngine;

public class ItemsController : MonoBehaviour, IInteractable
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
    public bool isDropable = false;

    [Range(0, 100)]
    public float dropChance = 0f;
    int layer;

    public Vector3 equipedPosition;
    public Vector3 equipedRotation;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        layer = gameObject.layer;
    }

    void Awake()
    {
        heathRecover = 20;
        // alreadyPurchased = false;
    }

    public void UseItem()
    {
        Debug.Log($"Usou {name}");
        GameManager.instance.SetHealth(heathRecover);
        Destroy(gameObject);
    }

    public void OnEquip(int layer)
    {
        rb.isKinematic = true;
        transform.SetLocalPositionAndRotation(equipedPosition, Quaternion.Euler(equipedRotation));
        gameObject.layer = layer;
    }

    public void OnDrop()
    {
        rb.isKinematic = false;
        gameObject.layer = layer;
        transform.SetParent(null);
    }

    public void Interact()
    {
        PlayerBehavior.instance.playerItem.Equip(this);
    }
}
