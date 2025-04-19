using Zenject;

public class PlayerHandlersService
{
    private PlayerDefaultRadiusDamageHandler _defaultRadiusDamageHandler;
    
    public PlayerDefaultRadiusDamageHandler DefaultRadiusDamageHandler => _defaultRadiusDamageHandler;
    
    public PlayerHandlersService(PlayerContainer playerContainer, OverlapSphereHandler overlapSphereHandler)
    {
        _defaultRadiusDamageHandler = new PlayerDefaultRadiusDamageHandler(playerContainer, overlapSphereHandler);
    }
}