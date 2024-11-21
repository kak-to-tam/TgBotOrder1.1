namespace tgBotOrder_v11.MachineState;


public interface IState
{

    public void Entry(IState prev);
    public void Handler();
    public void Exit(IState nextState);
    public void Reset(IState startState);


}

