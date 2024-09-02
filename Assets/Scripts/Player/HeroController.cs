using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

#region HeroController Class
public class HeroController : BaseController, IHealth
{
    #region Fields
    private IState currentState;
    private Vector2 moveDirection;
    public Transform spawnPoint;
    private Transform target;
    public bool isAttacking;
    private bool canAttack;
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    [Header("Health")]
    public Slider healthSlider;
    public Transform cameraTransform;
    #endregion

    #region Skills
    [Header("Skills")]
    [SerializeField] private List<SkillManager> skillManagers;
    private SkillCooldowns cooldownManager;
    public bool changeScale;
    public GameObject teleportPrefab;
    #endregion

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
        currentState = new IdleState(this);
        moveSpeed = 7f;
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

        cooldownManager = gameObject.AddComponent<SkillCooldowns>();
        cooldownManager.Initialize(skillManagers);

        UpdateHealthSlider();
    }
    #endregion

    #region Update Loop
    private void Update()
    {
        currentState.Execute();

        if (!isAttacking && IsAttacking())
        {
            ChangeState(new IdleState(this));
        }

        if (healthSlider != null && cameraTransform != null)
        {
            healthSlider.transform.LookAt(cameraTransform);
            healthSlider.transform.Rotate(0, 180, 0);
        }
    }
    #endregion

    #region State Management
    public void ChangeState(IState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    #endregion

    #region Movement
    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;
    }

    public void Move()
    {
        if (moveDirection != Vector2.zero)
        {
            Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);
            rb.MovePosition(transform.position + movement * Time.deltaTime * moveSpeed);
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(newRotation);

            animator.SetTrigger("IsMoving");
        }
        else
        {
            animator.ResetTrigger("IsMoving");
        }
    }

    public bool HasInput()
    {
        return moveDirection != Vector2.zero;
    }
    #endregion

    #region Attacking
    public bool IsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    public void OnAttack(string attackType)
    {
        if (cooldownManager.IsSkillOnCooldown(attackType))
        {
            return;
        }
        isAttacking = true;
        RotateToTarget();
        UseSkill(attackType);
        ChangeState(new AttackState(this));
        cooldownManager.StartCooldown(attackType);
    }

    public void UseSkill(string skillName)
    {
        switch (skillName)
        {
            case "Heal":
                Debug.Log("Using Heal skill.");
                Heal(50);
                break;
            case "Auxiliary":
                GameObject teleport = Instantiate(teleportPrefab, transform);
                teleport.transform.position = transform.position;
                // Unfinished
                break;
            case "Normal":
                animator.SetTrigger("IsAttacking");
                ObjectPool.Instance.SpawnFromPool("arrow", spawnPoint.position, spawnPoint.rotation, 1);
                break;
            case "Skill1":
                animator.SetTrigger("IsAttacking");
                ObjectPool.Instance.SpawnFromPool("arrow", spawnPoint.position, spawnPoint.rotation, 1);
                changeScale = true;
                break;
            case "Skill2":
                animator.SetTrigger("IsAttacking");
                ObjectPool.Instance.SpawnFromPool("arrow", spawnPoint.position, spawnPoint.rotation, 1);
                break;
            case "Skill3":
                animator.SetTrigger("IsAttacking");
                ObjectPool.Instance.SpawnFromPool("arrow", spawnPoint.position, spawnPoint.rotation, 1);
                break;
            default:
                Debug.LogWarning("Skill not recognized: " + skillName);
                break;
        }
    }

    public void CheckAttack(bool canAttack, Transform enemy)
    {
        this.canAttack = canAttack;
        target = enemy;
    }

    public void RotateToTarget()
    {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = targetRotation;
        }
    }
    #endregion

    #region Health Management
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            ChangeState(new DeadState(this));
        }
        UpdateHealthSlider();
    }

    public void Heal(float amount)
    {
        StartCoroutine(HealOverTime(amount));
    }

    private IEnumerator HealOverTime(float amount)
    {
        float healPerTick = amount / 5f;
        float totalHealed = 0f;

        while (totalHealed < amount)
        {
            float healThisTick = Mathf.Min(healPerTick, amount - totalHealed);
            CurrentHealth = Mathf.Min(CurrentHealth + healThisTick, MaxHealth);
            UpdateHealthSlider();
            totalHealed += healThisTick;

            yield return new WaitForSeconds(1f);
        }
    }

    public bool IsDead => CurrentHealth <= 0;

    public void Dead()
    {
        Debug.Log("Hero has died");
        animator.SetTrigger("Die");
        gameObject.SetActive(false);
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = CurrentHealth / MaxHealth;
        }
    }
    #endregion
}
#endregion
