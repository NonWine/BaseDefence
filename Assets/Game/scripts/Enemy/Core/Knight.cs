using System;
using UnityEngine;

public class Knight : BaseEnemy
{
    public override Type Type => typeof(Knight);
    
    private void OnTriggerEnter(Collider other)
    {
    }
    
}