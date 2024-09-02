using UnityEngine;

public class MoveState : IState
{
    private HeroController hero;

    public MoveState(HeroController hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        Debug.Log("Enter Move State");
    }

    public void Execute()
    {
        hero.Move();
        if (!hero.HasInput())
        {
            hero.ChangeState(new IdleState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Move State");
    }
}
