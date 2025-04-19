using UnityEngine;

public interface IDamageableHandler
{
    void HandDamage(int damageSend, out bool isDetected, out IDamageable[] targets);

}