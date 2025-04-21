public interface IPlayerMonitor
{
    float AverageKillTime { get; }
    int AliveEnemies { get; }
    bool IsUnderPressure { get; }
}