using UnityEngine;

public class IdleState : IState
{
    private HeroController hero;

    public IdleState(HeroController hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle State");
    }

    public void Execute()
    {
        if (hero.HasInput())
        {
            hero.ChangeState(new MoveState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }
}
