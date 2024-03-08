using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ChaseEnemyState : EnemyState
{
    #region Variables
    private EnemyStateManager _enemy;

    private float _pathUpdateDeadline;
    private float _updatePathDelay = 0.2f;
    #endregion

    #region Inherited Functions
    public override void StateStart(EnemyStateManager enemy)
    {
        _enemy = enemy;
    }

    public override void StateUpdate()
    {
        StateHandler();
    }
    public override void OnCollisionEnter(Collision collision)
    {
 
    }

    #endregion

    private void UpdatePath()
    {
        if (Time.time >= _pathUpdateDeadline)
        {
            _pathUpdateDeadline = Time.time + _updatePathDelay;
            _enemy.NavMeshAgent.SetDestination(_enemy.Target.position);
        }
    }

    #region State Functions
    private void StateHandler()
    {
        if (_enemy.Target != null)
        {
            bool inRange = Vector3.Distance(_enemy.transform.position, _enemy.Target.position) <= _enemy.ShootDistance;
            if (inRange)
            {
                _enemy.SetState(_enemy.AttackState);
            }
            else
            {
                UpdatePath();
            }
        }
    }
    #endregion
}
