using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : Stats
{
     public float RadiusFarming { get; private set; }

     public float MiningCD { get; private set; }
     public float ToolsDamage;
//    [SerializeField] public float CoolDown;
}