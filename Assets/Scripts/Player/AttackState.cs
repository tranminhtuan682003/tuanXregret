using UnityEngine;

public class AttackState : IState
{
    private HeroController hero;

    public AttackState(HeroController hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        Debug.Log("Enter Attack State");
    }

    public void Execute()
    {
        if (!hero.IsAttacking())
        {
            hero.ChangeState(new IdleState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Attack State");
    }
}
