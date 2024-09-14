using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public BagController bagController;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CanPickUp"))
        {
            if(other.GetComponent<ItemsController>().alreadyPurchased){
                bagController.PutItemOnBag(other.gameObject);
            }
        }
    }
}
