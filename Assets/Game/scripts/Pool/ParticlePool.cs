using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;

    [SerializeField] private ParticleSystem[] _poofFx;
    [SerializeField] private ParticleSystem[] _bloodfFx;
    [SerializeField] private ParticleSystem[] _sparkleFx;
    [SerializeField] private ParticleSystem[] fireArrow;
    [SerializeField] private ParticleSystem[] explosionFx;
    [SerializeField] private ParticleSystem[] frozenExplosiveFx;
    [SerializeField] private ParticleSystem[] deadZombieFx;
    [SerializeField] private ParticleSystem[] polzunBloodHitFx;
    [SerializeField] private ParticleSystem[] polzunDeadZombieFx;
    [SerializeField] private ParticleSystem[] mimicDeadFx;
    [SerializeField] private ParticleSystem[] explosiveSmolaFx;
    [SerializeField] private ParticleSystem[] explosiveBulletFx;
    [SerializeField] private ParticleSystem[] bombFx;
    [SerializeField] private ParticleSystem[] canistraExplosionFx;
    [SerializeField] private ParticleSystem[] spawnItemFx;

    
    private int _currentPoof;
    private int _currentBlood;
    private int _currentSparkle;
    private int currentBlood;
    private int currentBombFx;
    private int currentExplosiveSmola;
    private int currentFire;
    private int currentExplossion;
    private int currentfrozen;
    private int currentZombie;
    private int currentZombiePolzun;
    private int currentBloodPolzun;
    private int currentMimicDead;
    private int currentExplosiveBullet;
    private int currentCanistraFx;
    private int currentSpawnItemFx;

    public void PlaySpawnItemFx(Vector3 pos)
    {
        spawnItemFx[currentSpawnItemFx].transform.position = pos;
        spawnItemFx[currentSpawnItemFx].Play();
        currentSpawnItemFx++;
        if (currentSpawnItemFx == spawnItemFx.Length)
            currentSpawnItemFx = 0;
    }

    public void PlayCanistraFx(Vector3 pos)
    {
        canistraExplosionFx[currentCanistraFx].transform.position = pos;
        canistraExplosionFx[currentCanistraFx].Play();
        currentCanistraFx++;
        if (currentCanistraFx == canistraExplosionFx.Length)
            currentCanistraFx = 0;
    }
    
    public void PlayBombFx(Vector3 pos)
    {
        bombFx[currentBombFx].transform.position = pos;
        bombFx[currentBombFx].Play();
        currentBombFx++;
        if (currentBombFx == bombFx.Length)
            currentBombFx = 0;
    }

    public void PlayExplosiveBullet(Vector3 pos)
    {
        explosiveBulletFx[currentExplosiveBullet].transform.position = pos;
        explosiveBulletFx[currentExplosiveBullet].Play();
        currentExplosiveBullet++;
        if (currentExplosiveBullet == explosiveBulletFx.Length)
            currentExplosiveBullet = 0;

    }

    public void PlayExplosiveSmola(Vector3 pos)
    {
        explosiveSmolaFx[currentExplosiveSmola].transform.position = pos;
        explosiveSmolaFx[currentExplosiveSmola].Play();
        currentExplosiveSmola++;
        if (currentExplosiveSmola == explosiveSmolaFx.Length)
            currentExplosiveSmola = 0;

    }
    public void MimicDead(Vector3 pos)
    {
        mimicDeadFx[currentMimicDead].transform.position = pos;
        mimicDeadFx[currentMimicDead].Play();
        currentMimicDead++;
        if (currentMimicDead == mimicDeadFx.Length)
            currentMimicDead = 0;
    }
    public void PlayBloodPolzun(Vector3 pos)
    {
        polzunBloodHitFx[currentBloodPolzun].transform.position = pos;
        polzunBloodHitFx[currentBloodPolzun].Play();
        currentBloodPolzun++;
        if (currentBloodPolzun == polzunBloodHitFx.Length)
            currentBloodPolzun = 0;
    }

    public void PlayDeadPolzun(Vector3 pos)
    {
        polzunDeadZombieFx[currentZombiePolzun].transform.position = pos;
        polzunDeadZombieFx[currentZombiePolzun].Play();
        currentZombiePolzun++;
        if (currentZombiePolzun == polzunDeadZombieFx.Length)
            currentZombiePolzun = 0;
    }

    public void PlayDeadZombie(Vector3 pos)
    {
        deadZombieFx[currentZombie].transform.position = pos;
        deadZombieFx[currentZombie].Play();
        currentZombie++;
        if (currentZombie == deadZombieFx.Length)
            currentZombie = 0;
    }
    public void PlayFrozenExplose(Vector3 pos)
    {
        frozenExplosiveFx[currentfrozen].transform.position = pos;
        frozenExplosiveFx[currentfrozen].Play();
        currentfrozen++;
        if (currentfrozen == frozenExplosiveFx.Length)
            currentfrozen = 0;
    }

    public void PlayExplossion(Vector3 pos, float radius)
    {
        explosionFx[currentExplossion].transform.position = pos;
        explosionFx[currentExplossion].transform.localScale = new Vector3(radius, radius, radius);
        explosionFx[currentExplossion].Play();
        currentExplossion++;
        if (currentExplossion == explosionFx.Length)
            currentExplossion = 0;
    }

    public void PlayBlood(Vector3 pos)
    {
        _bloodfFx[currentBlood].transform.position = pos;
        _bloodfFx[currentBlood].Play();
        currentBlood++;
        if (currentBlood == _bloodfFx.Length)
            currentBlood = 0;
    }
    


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


    
    public void PlaySparkle(Vector3 pos)
    {
        _sparkleFx[_currentSparkle].transform.position = pos;
        _sparkleFx[_currentSparkle].Play();
        _currentSparkle++;
        if (_currentSparkle == _sparkleFx.Length)
            _currentSparkle = 0;
    }
    
}
