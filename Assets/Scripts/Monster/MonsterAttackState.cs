using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : IState
{
    private MonsterController monster;
    public MonsterAttackState(MonsterController monster)
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
