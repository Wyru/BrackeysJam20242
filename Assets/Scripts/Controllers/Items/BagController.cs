using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BagController : MonoBehaviour
{
    public static BagController instance;
    public List<GameObject> itemsOnBag = new List<GameObject>();
    public TMP_Text _dropBagText;
    private GameObject handPosition;
    public GameObject bag;
    public bool bagOnHand;

    void Awake()
    {
        instance = this;
        handPosition = GameObject.FindGameObjectsWithTag("PaperBagSlot")[0];
        var defaultCanvasBehavior = FindAnyObjectByType<DefaultCanvasBehavior>();
        if (defaultCanvasBehavior != null)
        {
            _dropBagText = defaultCanvasBehavior.bagText;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            DropBag();
        }
    }

    public void RemoveItems()
    {
        if(itemsOnBag.Count > 0){
            PickUpScript.instance.PickUpObject(itemsOnBag[0]);
            itemsOnBag[0].SetActive(true);
            itemsOnBag.RemoveAt(0);
        }
    }

    public void PickUpBag()
    {
        bagOnHand = true;
        _dropBagText.enabled = true;
        _dropBagText.text = "T to drop bag";
        gameObject.transform.SetParent(handPosition.transform, false);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        bag.transform.localPosition = gameObject.transform.localPosition;
        bag.SetActive(false);
    }

    public void DropBag()
    {
        bagOnHand = false;
        _dropBagText.enabled = false;
        _dropBagText.text = "";
        gameObject.transform.rotation = Quaternion.identity;
        bag.transform.rotation = Quaternion.identity;
        gameObject.transform.SetParent(null);
        bag.GetComponent<Rigidbody>().isKinematic = false;   
        bag.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CanPickUp"))
        {
            Debug.Log("Test");
            if(other.GetComponent<ItemsController>().alreadyPurchased){
                itemsOnBag.Add(other.gameObject);
            }
        }
    }
}
