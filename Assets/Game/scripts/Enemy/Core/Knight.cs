using System;
using UnityEngine;

public class Knight : BaseEnemy
{
    public override Type Type => typeof(Knight);
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BuildingBorder"))
        {
            if(UnitAgrRadius.CurrentAgredTransform == null)
                return;
            
            if (UnitAgrRadius.CurrentAgredTransform.TryGetComponent(out IBuildingDamagable buildingDamagable))
            {
                if(buildingDamagable.Team != Team)
                    EnemyStateMachine.ChangeState<AttackState>();
                
            }
            
        }
    }
    
}