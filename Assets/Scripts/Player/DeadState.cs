using UnityEngine;

public class DeadState : IState
{
    private HeroController hero;

    public DeadState(HeroController hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        Debug.Log("Enter Dead State");
    }

    public void Execute()
    {
        hero.Dead();
    }

    public void Exit()
    {
        Debug.Log("Exit Dead State");
    }
}
