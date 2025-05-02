using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerExpa : MonoBehaviour
{
    [Inject] private PlayerLevelController playerLevelController;
    [SerializeField] private float amplifier;

    private void Start()
    {
        playerLevelController.ImproveModificator(amplifier);
    }
    private void OnDisable()
    {
        playerLevelController.ImproveModificator(-amplifier);
    }
}
