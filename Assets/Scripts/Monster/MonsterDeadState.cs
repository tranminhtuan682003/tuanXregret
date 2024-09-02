using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeadState : IState
{
    private MonsterController monster;
    public MonsterDeadState(MonsterController monster)
    {
        this.monster = monster;
    }
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
