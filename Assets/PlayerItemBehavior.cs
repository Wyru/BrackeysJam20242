using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemBehavior : MonoBehaviour
{
    public string equippedItemLayerName = "Hold"; // Nome da camada
    private int equippedItemLayer; // Valor da camada
    public Transform itemSlot;
    public ItemsController currentItem;

    void Start()
    {
        equippedItemLayer = LayerMask.NameToLayer(equippedItemLayerName);

        if (equippedItemLayer == -1)
        {
            Debug.LogError($"A camada '{equippedItemLayerName}' não existe! Certifique-se de que está configurada nas camadas do projeto.");
        }
    }

    void Update()
    {
        if (PlayerBehavior.instance.dropItemInput.action.WasPerformedThisFrame())
        {
            DropItem();
        }

        if (PlayerBehavior.instance.useItemInput.action.WasPerformedThisFrame())
        {
            UseItem();
        }

    }

    public void Equip(ItemsController item)
    {
        if (currentItem != null)
            DropItem();

        item.transform.SetParent(itemSlot.transform);
        item.OnEquip(equippedItemLayer);
        currentItem = item;
    }


    public void DropItem()
    {
        if (currentItem == null)
            return;

        var item = currentItem;
        currentItem.OnDrop();
        var foward = PlayerBehavior.instance.playerCameraBehavior.playerCamera.transform.forward;

        item.rb.AddForce(
            foward * 1,
            ForceMode.Impulse
        );
        currentItem = null;
    }

    public void UseItem()
    {
        if (currentItem == null)
            return;

        currentItem.UseItem();
        currentItem.OnDrop();
        currentItem = null;
    }


}
