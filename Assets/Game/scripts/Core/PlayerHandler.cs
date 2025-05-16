using UnityEngine;
using Zenject;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;


    public Player Player => playerPrefab;
    
    private void Start()
    {
        //Player.PlayerContainer.Agent.Warp(playerSpawnPoint.position);
        playerPrefab.Initialize();
    }
}