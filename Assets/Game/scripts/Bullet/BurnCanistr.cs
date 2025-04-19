using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BurnCanistr : Bullet
{
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform _canistaPos;
    private List<Transform> bisyPoints = new List<Transform>();


    private void Start()
    {
        
    }

    public override void Spam(Transform posThrow)
    {
        for (int i = 0; i < Attribute(ValueAttribute.ItemCount).Value; i++)
        {
            // posThrow = _canistaPos;
            // GameObject benzin = Instantiate(bullet, posThrow.position, posThrow.rotation, Player.Instance.transform);
            // CanistraBullet canistra = benzin.GetComponent<CanistraBullet>();
            // canistra.SetPerSec(Attribute(ValueAttribute.PerSecDamage).Value);
            // canistra.SetDamage(Damage);
            // canistra.SetTimeToLive(Attribute(ValueAttribute.TimeDuration).Value);
            // canistra.SetRadius(Attribute(ValueAttribute.Radius).Value);
            // Rigidbody benzinBody = benzin.GetComponent<Rigidbody>();
            // Debug.Log(DefinePoint(i).position);
            // benzinBody.velocity = (DefinePoint(i).position - benzin.transform.position).normalized * Speed;
            // Debug.Log("spawnCAnistr");
        }
        bisyPoints.Clear();
    }

    private Transform DefinePoint(int i)
    {

        return points[i];
    }

}
