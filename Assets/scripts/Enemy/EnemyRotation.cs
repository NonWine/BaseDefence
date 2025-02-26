using UnityEngine;

public class EnemyRotation
{
    private BaseEnemy _baseEnemy;

    public EnemyRotation(BaseEnemy baseEnemy)
    {
        _baseEnemy = baseEnemy;
    }
    
    public void TargetRotation(Transform AimTarget)
    {
        Vector3 direction = (AimTarget.position - _baseEnemy.transform.position ).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _baseEnemy.transform.rotation = Quaternion.Slerp(   _baseEnemy.transform.rotation, targetRotation, _baseEnemy.EnemyStatsConfig.RotateSpeed * Time.deltaTime);
    }

}