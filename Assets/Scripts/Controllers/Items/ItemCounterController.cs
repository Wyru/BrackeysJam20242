using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCounterController : MonoBehaviour
{
    public List<string> Response;
    public List<GameObject> itemsOnCounter = new List<GameObject>();
    public GameObject cartTotalObject;
    public Transform bagPositionSpawn;
    public GameObject bagToSpawn;
    private int cartTotalValue;
    private int playerMoney;
    private GameManager _gameManager;

    private bool _alreadySpawned = false;
    public GameObject _bagExist;

    void Awake()
    {
        _bagExist = null;
    }

    private void Start()
    {
        cartTotalObject = FindObjectOfType<DefaultCanvasBehavior>().cartTotalObject;
    }

    void Update()
    {
        if (itemsOnCounter.Count > 0)
        {
            cartTotalObject.SetActive(true);
            cartTotalObject.GetComponent<TMP_Text>().text = "Cart Total: $" + cartTotalValue;
        }
        else
        {
            cartTotalObject.SetActive(false);
        }
        if (_bagExist = GameObject.FindWithTag("PaperBag"))
        {
            _alreadySpawned = true;
        }
        else
        {
            _alreadySpawned = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 17){

        }else{
            ItemsController item = other.GetComponent<ItemsController>();
            if (item != null && !item.alreadyPurchased)
            {
                itemsOnCounter.Add(other.gameObject);
                cartTotalValue += item.price;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        ItemsController item = other.GetComponent<ItemsController>();
        if (item != null && itemsOnCounter.Contains(other.gameObject))
        {
            itemsOnCounter.Remove(other.gameObject);
            cartTotalValue -= item.price;
        }
    }

    public bool IsItemOnCounter(GameObject item)
    {
        return itemsOnCounter.Contains(item);
    }

    public void CompletePurchase()
    {
        if (!_alreadySpawned)
        {
            GameObject paperBag = Instantiate(bagToSpawn, bagPositionSpawn);
            if (cartTotalValue <= _gameManager.moneyTotal)
            {
                for (int i = 0; i < itemsOnCounter.Count; i++)
                {
                    paperBag.gameObject.GetComponent<BagController>().itemsOnBag.Add(itemsOnCounter[i]);
                    itemsOnCounter[i].transform.SetParent(paperBag.gameObject.transform, true);
                    itemsOnCounter[i].gameObject.transform.localPosition = Vector3.zero;
                    itemsOnCounter[i].gameObject.SetActive(false);
                    itemsOnCounter[i].gameObject.GetComponent<ItemsController>().alreadyPurchased = true;
                    paperBag.SetActive(true);
                }
                _alreadySpawned = true;
                _gameManager.SetMoneyToday(-cartTotalValue);
                cartTotalValue = 0;
                itemsOnCounter.Clear();
                DialogSystemController.ShowDialogs(Response);
            }
        }
        else
        if (cartTotalValue <= _gameManager.moneyTotal && _alreadySpawned)
        {
            Debug.Log("Test2");
            for (int i = 0; i < itemsOnCounter.Count; i++)
            {
                itemsOnCounter[i].gameObject.GetComponent<ItemsController>().alreadyPurchased = true;
            }
            _gameManager.SetMoneyToday(-cartTotalValue);
            cartTotalValue = 0;
            itemsOnCounter.Clear();
        }
    }

    void OnEnable()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }

    void OnDisable()
    {
        _gameManager = null;
    }

}
