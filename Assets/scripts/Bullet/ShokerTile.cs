using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShokerTile : MonoBehaviour
{
    private int dmg;
    private bool inTrigger;
    private float _radius;
    private Collider other;
    private Vector3 closestTarget;
    private Transform tardetDetected;
    private float point;
    private Rigidbody rd;
    private float _speed;
    private int maxRc, currentRc;
    
    private void Start()
    {
        Destroy(gameObject, 6f);
        rd = GetComponent<Rigidbody>();
        point = Mathf.Infinity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Dire") && other != this.other)
        {
            Debug.Log("target" + other.gameObject.name);
            if (currentRc >= maxRc)
                Destroy(gameObject);
           
            this.other = other;
            TryFindTheNearlestEnemy();
            if (tardetDetected != null)
            {

                rd.velocity = ((new Vector3(tardetDetected.position.x, 3.5f, tardetDetected.position.z) - new Vector3(transform.position.x, 3.5f, transform.position.z))).normalized * _speed;
                other.transform.GetComponent<BaseEnemy>().GetDamage(dmg);
                currentRc++;
                inTrigger = false;
                tardetDetected = null;

            }
            else
            {
                other.transform.GetComponent<BaseEnemy>().GetDamage(dmg);
                                Destroy(gameObject);

            }
        }
    }

    public void TryFindTheNearlestEnemy()
    {
        point = Mathf.Infinity;
        Collider[] enemys = Physics.OverlapSphere(transform.position, _radius);
        foreach (var item in enemys)
        {
            if (item.CompareTag("Dire") && (item.gameObject != other.gameObject))
            {

                inTrigger = true;
                break;
            }
            else
                inTrigger = false;
        }
        if (inTrigger)
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys[i].CompareTag("Dire") && (( enemys[i].gameObject != other.gameObject ) && !enemys[i].GetComponent<BaseEnemy>().IsDeath))
                {
                    Vector3 close = enemys[i].ClosestPoint(transform.position);
                    Vector3 dir = transform.position - close;

                    if (point > dir.magnitude) //Can Check if the target is alive? 
                    {
                        point = dir.magnitude;
                        closestTarget = enemys[i].transform.position;
                        tardetDetected = enemys[i].transform;
                    }
                }
            }
        if (!inTrigger)
            Destroy(gameObject);
    }

    public void SetMaxRc(int value) => maxRc = value;

    public void SetDamage(int value) => dmg = value;

    public void SetRadius(float value) => _radius = value;

    public void SetSpeed(float speed) => _speed = speed;
}
