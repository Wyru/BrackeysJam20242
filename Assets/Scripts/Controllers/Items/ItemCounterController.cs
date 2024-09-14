using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCounterController: MonoBehaviour
{
    public List<GameObject> itemsOnCounter = new List<GameObject>();
    public GameObject cartTotalObject;
    public Transform bagPositionSpawn;
    public GameObject bagToSpawn;
    private int cartTotalValue;
    private int playerMoney;
    private GameManager _gameManager;

    void Update()
    {
        if(itemsOnCounter.Count > 0)
        {
            cartTotalObject.SetActive(true);
            cartTotalObject.GetComponent<TMP_Text>().text = "Cart Total: $" + cartTotalValue;
        }else{
            cartTotalObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        ItemsController item = other.GetComponent<ItemsController>();
        if (item != null && !item.alreadyPurchased)
        {
            itemsOnCounter.Add(other.gameObject);
            cartTotalValue += item.price;
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
        GameObject paperBag = Instantiate(bagToSpawn, bagPositionSpawn);
        if(cartTotalValue <= _gameManager.moneyTotal){
            paperBag.gameObject.GetComponent<BagController>().itemsOnBag = itemsOnCounter;
            for (int i = 0; i < itemsOnCounter.Count; i++)
            {
                itemsOnCounter[i].transform.SetParent(paperBag.gameObject.transform,true);
                itemsOnCounter[i].gameObject.transform.localPosition = Vector3.zero;
                itemsOnCounter[i].gameObject.SetActive(false);
                itemsOnCounter[i].gameObject.GetComponent<ItemsController>().alreadyPurchased = true;
                paperBag.SetActive(true);
            }
            _gameManager.SetMoneyToday(-cartTotalValue);
            cartTotalValue = 0;
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
