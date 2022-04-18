using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpComponent : MonoBehaviour
{
    [SerializeField] private ItemScript pickupItem;

    [Tooltip("Manual Orverride for drop amount, if left at -1 it will used thha mount forom the scriptable object")]
    [SerializeField]
    private int amount = -1;

    [SerializeField] private MeshRenderer propMeshRenderer;
    [SerializeField] private MeshFilter propMeshFilter;

    private ItemScript itemInstance;
    

// Start is called before the first frame update
    void Start()
    {
        InstantiateItem();
    }

    private void InstantiateItem()
    {
        itemInstance = Instantiate(pickupItem);
        if (amount>0)
        {
            itemInstance.SetAmount(amount);
        }
        ApplyMesh();
    }

    void ApplyMesh()
    {
        if (propMeshFilter) propMeshFilter.mesh = pickupItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        if (propMeshRenderer)
            propMeshRenderer.materials = pickupItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        InventoryComponent playerInventory = other.GetComponent<InventoryComponent>();

        if (playerInventory)
        {
            playerInventory.AddItem(itemInstance, amount);
        }
        //add to inventory here
        //get reference to the player inventory, add item to it
        
        Destroy(gameObject);
    }
}
