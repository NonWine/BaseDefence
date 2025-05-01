using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StinkyCloud : MonoBehaviour
{ 
    [SerializeField] private LayerMask damageableMask;
    [SerializeField] private float radiusVenom = 5f;
    [SerializeField] private int _periodicDamage;
    private Collider[] _overlapResults = new Collider[20];
     private float _timer;
    private float _existTimer;
    [SerializeField] private float _existTime;
    [SerializeField] private float _damageInterval;
    [SerializeField] private ParticleSystem _cloudEffect;
    [Inject] private GameController gameController;
    private Transform parent;

    private void OnEnable()
    {
        transform.localScale = new Vector3(radiusVenom * 2, transform.localScale.y, radiusVenom * 2);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        var main = _cloudEffect.main;
        main.duration = _existTime;
        _cloudEffect.Play();
    }
    private void OnDisable()
    {

    }
    private void Damaging()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, radiusVenom, _overlapResults, damageableMask);
        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out IDamageable targetDamageable))
            {
                targetDamageable.GetDamage(_periodicDamage);
            }
        }
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _existTimer += Time.deltaTime;
        if(_timer >= _damageInterval)
        {
            Damaging();
            _timer = 0;
        }
        if(_existTimer >= _existTime)
        {
            Destroy(gameObject);
        }
    }
}
