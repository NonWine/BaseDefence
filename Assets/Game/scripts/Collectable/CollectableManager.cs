using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] private WalletObj[] _walletObjs;
    [SerializeField] private CollectableWallet _walletPrefab;
    [Inject] private DiContainer _diContainer;
    
    private List<CollectableWallet> _collectableWallets;

    public static CollectableManager Instace { get; set; }

    private void Start()
    {
        Instace = this;
        _collectableWallets = new List<CollectableWallet>();
        foreach (var wallet in _walletObjs)
        {
           var wall =  _diContainer.InstantiatePrefabForComponent<CollectableWallet>(_walletPrefab, transform);
           wall.Init(wallet);
            _collectableWallets.Add(wall);
        }
    }
    

    public CollectableWallet GetWallet(eCollectable collectable)
    {
        foreach (var wallet in _collectableWallets)
        {
            if (wallet.WalletType == collectable)
                return wallet;
        }
    
        Debug.LogError("Collectable wallet wasn't found!");
        return null;
    }
    
    [Button]
    public void Add100Value(eCollectable eCollectable)
    {
        _collectableWallets.Find(x => x.WalletType == eCollectable).Add(100);
    }
    
}