using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController, IHealth
{
    private IState currentState;
    private NavMeshAgent navMeshAgent;
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    private bool isFollowing;
    private Transform target;

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = new MonsterIdleState(this);
    }

    void Update()
    {
        currentState.Execute();
    }

    #region State Management
    public void ChangeState(IState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    #endregion

    #region MoveManager
    private void Move()
    {

    }

    public void SetTarget(bool isFollowing, Transform target)
    {
        this.isFollowing = isFollowing;
        this.target = target;
    }

    private void FollowTarget()
    {
        if (isFollowing && target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Move();
        }
    }
    #endregion

    #region HealthManager

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Dead();
        }
    }

    public bool IsDead => CurrentHealth <= 0;

    public void Dead()
    {
        Debug.Log("Hero has died");
        animator.SetTrigger("Die");
        gameObject.SetActive(false);
    }

    #endregion
}
