using System;
using UnityEngine;
using Zenject;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [Inject] private ScreenTouchController _screenTouchController;

    private void Start()
    {
        _screenTouchController.OnLeftSideTap += MoveLeft;
        _screenTouchController.OnRightSideTap += MoveRight;
    }

    private void OnDestroy()
    {
        _screenTouchController.OnLeftSideTap -= MoveLeft;
        _screenTouchController.OnRightSideTap -= MoveRight;
    }

    private void MoveRight()
    {
        transform.position += Vector3.back * (_speed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        transform.position += Vector3.forward * (_speed * Time.deltaTime);
    }
}