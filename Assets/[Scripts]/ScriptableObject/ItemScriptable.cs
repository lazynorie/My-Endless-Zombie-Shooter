using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    None, Weapon, Consumable, Equipment, Ammo
}
public abstract class ItemScriptable : ScriptableObject
{
    public string name = "Item";
    public ItemCategory itemCategory = ItemCategory.None;
    public GameObject itemPrefab;
    public bool stackable;
    public int maxSize = 1;

    public delegate void AmountChange();
    public event AmountChange OnAmountChange;

    public delegate void ItemDestroyed();
    public event ItemDestroyed OnItemDestroyed;
    
    public delegate void ItemDrropped();
    public event ItemDrropped OnItemDropped;

    public int amountValue = 1;
    
    public PlayerController controller { get; private set; }

    public virtual void Initialize(PlayerController playerController)
    {
        controller = playerController;
    }

    public abstract void UseItem(PlayerController playerController);

    public virtual void DeleteItem(PlayerController playerController)
    {
        OnItemDestroyed?.Invoke();
        //Delete item from inventory system here
    }

    public virtual void DropItem(PlayerController playerController)
    {
        OnItemDropped?.Invoke();
    }

    public void ChhangeAmount(int amount)
    {
        amountValue += amount;
        OnAmountChange?.Invoke();
    }

    public void SetAmount(int amount)
    {
        amountValue = amount;
        OnAmountChange?.Invoke();
    }
}
