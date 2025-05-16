using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public BaseEnemy SelectedEnemy;
    [ProgressBar(1, 100)] public int ChanceToSpawn;
    
    #region Editor

        [ShowInInspector] [PreviewField] [HideLabel]
        private GameObject Preview
        {
            get
            {
                if (SelectedEnemy != null)
                    return SelectedEnemy.gameObject;
                return GameObject;
            }
        }
        private GameObject GameObject;
    

    #endregion
}

