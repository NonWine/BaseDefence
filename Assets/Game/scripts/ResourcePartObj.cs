using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public abstract class ResourcePartObj : PoolAble , ITickable
{
     [field: SerializeField] public eCollectable TypeE { get; private set; }
    private bool _isPicked = true;
    private bool _canPickUp;
    public bool IsPicked => _isPicked;
    
    
    [Inject] private CollectableManager _collectableWallet;
    [Inject] private GameController GameController;

    private void Awake()
    {
        GameController.RegisterInTick(this);
    }

    protected virtual void Rotate()
    {
        transform.Rotate(Vector3.up, 300 * Time.deltaTime);
    }


    public void Tick()
    {

        Rotate();
    }

    public override void ResetPool()
    {
        gameObject.SetActive(true);
    }
    
    public async void PickUp()
    {
     //   await UniTask.Delay(Random.Range(0, 1000));
     _isPicked = true;
     _collectableWallet.GetWallet(TypeE).SendOne(transform);
     Debug.Log("Pick this ");
     DestroyAnim();
    }

    public void DestroyAnim()
    {
            transform.DOScale(0f, 0.25f).SetEase(Ease.InBack).OnComplete(() => { gameObject.SetActive(false); });
    }

    public void SetIdle()
    {
        _isPicked = false;
    }

}

