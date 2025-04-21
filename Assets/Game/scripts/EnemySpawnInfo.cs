using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public BaseEnemy SelectedEnemy;
    public int Count;

    
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

