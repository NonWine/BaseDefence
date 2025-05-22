using System;
using UnityEngine;

public class PlayerMoving : IMoveable
{
    private PlayerContainer _playerContainer;

    public PlayerMoving(PlayerContainer playerContainer)
    {
        _playerContainer = playerContainer;
    }
    
    public void Move()
    {
      //  _playerContainer.Magmitude = Mathf.Max(_playerContainer.Joystick.Vertical,_playerContainer.Joystick.Horizontal);
           // _playerContainer.Direction = new Vector3(_playerContainer.Joystick.Horizontal, _playerContainer.Joystick.Vertical, 0).normalized;
        //_playerContainer.Agent.Move(   _playerContainer.Direction * (10 * Time.deltaTime));
        _playerContainer.Rigidbody.MovePosition(_playerContainer.transform.position + _playerContainer.Direction * (10 * Time.deltaTime));
    }
}