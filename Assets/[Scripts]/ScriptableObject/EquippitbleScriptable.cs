using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquippitbleScriptable : ItemScriptable
{
  private bool isEquipped;
  public bool equipped
  {
    get => equipped;
    set
    {
      OnEquipStatusChange?.Invoke();
    } 
  }

  public delegate void EquipStatusChange();
  public event EquipStatusChange OnEquipStatusChange;

  public override void UseItem(PlayerController playerController)
  {
    isEquipped = !isEquipped;
  }
}
