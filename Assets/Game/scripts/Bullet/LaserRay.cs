using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LaserRay : MonoBehaviour, ITickable
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _laserOrigin;
    [SerializeField] private float _gunRange;
    [SerializeField] private float _laserDuration;
    [SerializeField] private LineRenderer _laserLine;
    [SerializeField] private float _shootingInterval;
    [SerializeField] private float _shootingTimer;
    [Inject] GameController gameController;
    private void Awake()
    {
        gameController.RegisterInTick(this);
    }
    public void Tick()
    {
        _shootingTimer += Time.deltaTime;
        if(_shootingTimer >= _shootingInterval)
        {
            _shootingTimer = 0;
            _laserLine.SetPosition(0, _laserOrigin.position);
            Vector3 rayOrigin = _playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, _playerCamera.transform.forward, out hit, _gunRange))
            {
                _laserLine.SetPosition(1, hit.point);
                Debug.Log("laser hit " + hit.transform.name);
            }
            else
            {
                _laserLine.SetPosition(1, rayOrigin + (_playerCamera.transform.forward * _gunRange));
            }
        }
    }
}
