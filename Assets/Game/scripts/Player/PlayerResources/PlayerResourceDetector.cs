using DG.Tweening;
using UnityEngine;

public class PlayerResourceDetector
{
    private PlayerContainer _playerContainer;
    private OverlapSphereHandler _overlapSphereHandler;
    
    public PlayerResourceDetector(PlayerContainer playerContainer, OverlapSphereHandler overlapSphereHandler)
    {
        _playerContainer = playerContainer;
        _overlapSphereHandler = overlapSphereHandler;
    }

    public void FindResources()
    {
        var resources = _overlapSphereHandler.GetFilteredObjects<ResourcePartObj>(
            _playerContainer.transform.position,
            _playerContainer.PlayerStats.RadiusDetection,
            0,
            resource => !resource.IsPicked
        );

        foreach (var resource in resources)
        {
            resource.PickUp();
            resource.transform.DOMove(_playerContainer.transform.position, 0.25f).SetEase(Ease.InBack);
            Debug.Log("Picked up a resource!");
        }

    }
    
}