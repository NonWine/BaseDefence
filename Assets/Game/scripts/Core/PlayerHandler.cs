using UnityEngine;
using Zenject;

[DefaultExecutionOrder(-1)]
public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Inject] private DiContainer diContainer;

    public Player Player { get; private set; }
    
    [Inject]
    private void Construct()
    {
        Player = diContainer.InstantiatePrefabForComponent<Player>(playerPrefab);
        Player.PlayerContainer.Agent.Warp(playerSpawnPoint.position);
        Player.Initialize();
    }
}