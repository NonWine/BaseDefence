﻿using System;
using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [Inject] private PlayerHandler player;
    
    private void Start()
    {
        virtualCamera.Follow = player.Player.transform;
    }
    
}