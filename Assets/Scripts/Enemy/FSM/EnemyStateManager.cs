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
    public ChaseEnemyState ChaseState = new ChaseEnemyState();
    public AttackEnemyState AttackState = new AttackEnemyState();

    [Header("Enemy AI Attributes")]
    [SerializeField] public Transform Target;
    [SerializeField] public NavMeshAgent NavMeshAgent;
    [SerializeField] public float ShootDistance;
    public GameObject ProjectilePrefab;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        ShootDistance = NavMeshAgent.stoppingDistance;

        _currentState = PatrolState;
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
