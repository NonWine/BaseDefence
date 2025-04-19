using System;
using UnityEngine;using UnityEngine.UIElements;
[RequireComponent(typeof(BaseWeaponStats))]
public class Bullet : Skill
{
    [SerializeField] protected GameObject bullet;
    protected float timer;
    [SerializeField] protected bool _default;
    public int Damage => Mathf.FloorToInt(Attribute(ValueAttribute.Damage).Value);

    public float CoolDown => Attribute(ValueAttribute.CoolDown).Value;

    public float Speed => Attribute(ValueAttribute.Speed).Value;


    public virtual void Draw(BaseEnemy unit, Transform spawnPos)
    {
        // float heightK = 3f;
        // GameObject prefabArrow = Instantiate(bullet, spawnPos.position, Player.Instance.gameObject.GetComponent<PlayerController>().GetBody().rotation);
        // Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
        // CurrentArrow.SetDamage((int)_baseWeaponStats.GetAtribute(ValueAttribute.Damage).Value);
        //
        // Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        // bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * _baseWeaponStats.GetAtribute(ValueAttribute.Speed).Value;
    }

    public virtual void Spam(Transform posThrow) { }
    
    public  bool isReload()
    {
        if (timer >= _baseWeaponStats.GetAtribute(ValueAttribute.CoolDown).Value && (_baseWeaponStats.CurrentLevel > 0 || _default))
        {
            timer = 0f;
            return true;
        }
        else
            return false;
    }
    
    //BAD OPTIMIZATION
    private void Update()
    {
        timer += Time.deltaTime;
    }


}