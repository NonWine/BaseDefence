using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDefaultRadiusDamageHandler 
{
    private PlayerContainer _playerContainer;
    private OverlapSphereHandler _overlapSphereHandler;
    private IDamageableHandler _damageableHandlerImplementation;

    public PlayerDefaultRadiusDamageHandler(PlayerContainer playerContainer, OverlapSphereHandler overlapSphereHandler)
    {
        _playerContainer = playerContainer;
        _overlapSphereHandler = overlapSphereHandler;
    }



    private bool TryDamagingByRadius(int damage, out IDamageable[] taregt)
    {
        var enemies = _overlapSphereHandler.GetFilteredObjects<IDamageable>(
            _playerContainer.transform.position,
            _playerContainer.PlayerStats.RadiusDetection,
            0,
            enemy => !enemy.IsDeath
        );

        List<IDamageable> damagedTargets = new List<IDamageable>();
        foreach (var enemy in enemies)
        {
            enemy.GetDamage(damage);
            damagedTargets.Add(enemy);
        }

        taregt = damagedTargets.ToArray();
        return taregt.Length > 0;    
    }
    

    public void HandDamage(int damage, out bool isDetected, out IDamageable[] taregt)
    {
        isDetected = TryDamagingByRadius(damage, out taregt);
    }
    
}