using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
   private ZombieStateMachine zombieStateMachine;

   private ScoreManager _scoreManager;
   

   private void Awake()
   {
      zombieStateMachine = GetComponent<ZombieStateMachine>();
     
      _scoreManager = FindObjectOfType<ScoreManager>();
   }

   public override void Destroy()
   {
      zombieStateMachine.ChangeState(ZombieStateType.Dying);
      _scoreManager.currentZombieCount--;
   }
   
}
