using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Stats 
{
    public float MaxHealth { get; private set; }
    public float MoveSpeed  { get; private set; }
     public int RotateSpeed  { get; private set; }
    public float RadiusDetection  { get; private set; }
     public float DamageHit { get; private set; }

    public float CurrentHealth { get; set; }
}

[System.Serializable]
public class EnemyStats : Stats
{
    public float DistanceFromSpawn { get; private set; }
     public float TargetDistance { get; private set; }
    
     public float TimeBeetwenHit { get; private set; }
    

}