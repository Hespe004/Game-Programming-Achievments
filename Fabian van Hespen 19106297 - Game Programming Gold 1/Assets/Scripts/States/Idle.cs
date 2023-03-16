public class Idle : BaseState
{
    private PlayerMovement _sm;

    public Idle(PlayerMovement stateMachine) : base("Idle", stateMachine) { 
        _sm = (PlayerMovement)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //Logic for switching states
        if (_horizontalInputPressed || _verticalInputPressed)
            stateMachine.ChangeState(_sm.movingState);
    }
}
