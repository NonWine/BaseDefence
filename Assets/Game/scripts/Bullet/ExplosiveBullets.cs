// using UnityEngine;
//
// public class ExplosiveBullets : Bullet
// {
//     private void Awake()
//     {
//         isOpen = PlayerPrefsExtra.GetBool("ExplosiveBulletOpen", isOpen);
//         _throwCD = PlayerPrefs.GetFloat("throwCDExplosiveBullets", _throwCD);
//         _damage = PlayerPrefs.GetInt("ExplosiveBulletDamage", _damage);
//     }
//
//     public override void AddDamage(int value)
//     {
//         base.AddDamage(value);
//         PlayerPrefs.SetInt("ExplosiveBulletDamage", _damage);
//     }
//
//     public override void UnlockBullet()
//     {
//         base.UnlockBullet();
//
//         PlayerPrefsExtra.SetBool("ExplosiveBulletOpen", isOpen);
//     }
//
//     public override void ReduceThrowCD(float value)
//     {
//         base.ReduceThrowCD(value);
//         PlayerPrefs.SetFloat("throwCDExplosiveBullets", _throwCD);
//
//     }
//
// }
