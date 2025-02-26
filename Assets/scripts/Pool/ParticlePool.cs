using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;

    [SerializeField] private ParticleSystem[] _poofFx;
    [SerializeField] private ParticleSystem[] _bloodfFx;
    [SerializeField] private ParticleSystem[] _sparkleFx;

    private int _currentPoof;
    private int _currentBlood;
    private int _currentSparkle;


    private void Awake()
    {
        Instance = this;
    }

    public void PlayPoof(Vector3 pos)
    {
        _poofFx[_currentPoof].transform.position = pos;
        _poofFx[_currentPoof].Play();
        _currentPoof++;
        if (_currentPoof == _poofFx.Length)
            _currentPoof = 0;
    }

    public void PlayBlood(Vector3 pos)
    {
        _bloodfFx[_currentBlood].transform.position = pos;
        _bloodfFx[_currentBlood].Play();
        _currentBlood++;
        if (_currentBlood == _bloodfFx.Length)
            _currentBlood = 0;
    }
    
    public void PlaySparkle(Vector3 pos)
    {
        _sparkleFx[_currentSparkle].transform.position = pos;
        _sparkleFx[_currentSparkle].Play();
        _currentSparkle++;
        if (_currentSparkle == _sparkleFx.Length)
            _currentSparkle = 0;
    }
    
}
