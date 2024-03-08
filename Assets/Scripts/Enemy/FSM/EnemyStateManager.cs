using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyState _currentState;
    public PatrolEnemyState PatrolState = new PatrolEnemyState();
    public AlertEnemyState AlertState = new AlertEnemyState();
    public ChaseEnemyState ChaseState = new ChaseEnemyState();
    public AttackEnemyState AttackState = new AttackEnemyState();

    void Start()
    {
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
