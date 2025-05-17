
using UnityEngine;

public class PlayerController 
{ 
    private IMoveable _moveable;
    private IRotateable _rotateable;
    private IEntityAnimateable _animateable;
    private PlayerStateMachine _playerStateMachine;

    
    public  PlayerController(IMoveable moveable,
        IRotateable rotateable,
        IEntityAnimateable animateable,
        PlayerStateMachine playerStateMachine

        )
    {
        _animateable = animateable;
        _moveable = moveable;
        _rotateable = rotateable;
        _playerStateMachine = playerStateMachine;

    }   
    
    public void Tick()
    {
        //_moveable.Move();
       // _rotateable.Rotate();
        _animateable.UpdateAnimator();
        _playerStateMachine.CurrentState.LogicUpdate();
    //    _playerResourceDetector.FindResources();
    //   _playerFarmDetector.FindFarmingResources();
    }
}