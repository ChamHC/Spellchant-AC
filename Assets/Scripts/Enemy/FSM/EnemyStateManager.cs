using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [Header("FSM Attributes")]
    private EnemyState _currentState;
    public PatrolEnemyState PatrolState = new PatrolEnemyState();
    public AlertEnemyState AlertState = new AlertEnemyState();
    public ChaseEnemyState ChaseState = new ChaseEnemyState();
    public AttackEnemyState AttackState = new AttackEnemyState();

    [Header("Enemy AI Attributes")]
    public Transform Target;
    public GameObject ProjectilePrefab;
    [SerializeField, ReadOnly] public NavMeshAgent NavMeshAgent;
    [SerializeField, ReadOnly] public float ShootDistance;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();

        ShootDistance = NavMeshAgent.stoppingDistance;

        _currentState = ChaseState;
        _currentState.StateStart(this);
    }

    void Update()
    {
        _currentState.StateUpdate();
    }

    public void SetState(EnemyState state)
    {
        _currentState = state;
        _currentState.StateStart(this);
    }
}
