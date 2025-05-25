using UnityEngine;

public class PlayerRotating : IRotateable
{
    private PlayerContainer _playerContainer;
    private bool _isTargeting;
    
    public PlayerRotating(PlayerContainer playerContainer)
    {
        _playerContainer = playerContainer;
    }

    public void  Rotate()
    {
        if (!_isTargeting)
        {
            float angle = Mathf.Atan2(-(_playerContainer.Body[0].transform.position + new Vector3(0, 1)).y, 0) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            foreach (var bone in _playerContainer.Body)
            {

                bone.rotation = Quaternion.Slerp(bone.rotation,
                targetRotation,
                7f * Time.deltaTime);
            }
            return;
        }
        
        if (_playerContainer.Direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(_playerContainer.Direction.y, _playerContainer.Direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            foreach(var bone in _playerContainer.Body)
            {

                bone.rotation = Quaternion.Slerp(bone.rotation,
                targetRotation,
                7f * Time.deltaTime);
            }
        }
        else
        {
            float angle = Mathf.Atan2(-(_playerContainer.Body[0].transform.position + new Vector3(0, 1)).y, 0) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            foreach (var bone in _playerContainer.Body)
            {

                bone.rotation = Quaternion.Slerp(bone.rotation,
                targetRotation,
                7f * Time.deltaTime);
            }
            return;
        }


       

    }

    public void SetTargetRotate(Transform target)
    {
        _isTargeting = true;
        if(target ==null)
        {
            return;
        }
        /*Vector3 direction = (target.position - _playerContainer.Body.position).normalized; // Отримуємо напрямок
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up); // Обчислюємо правильний поворот

        _playerContainer.Body.rotation = Quaternion.Slerp(
            _playerContainer.Body.rotation,
            targetRotation,
            20 * Time.deltaTime
        );*/
    }


    public void UnLockTarget()
    {
        _isTargeting = false;

    }
}