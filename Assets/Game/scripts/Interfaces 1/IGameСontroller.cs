public interface IGameСontroller
{
    void RegisterInTick(IGameTickable gameTickable);

    void UnregisterFromTick(IGameTickable gameTickable);
}