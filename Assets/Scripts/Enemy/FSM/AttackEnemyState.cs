using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyState : EnemyState
{
    #region Variables
    private EnemyStateManager _enemy;
    private bool _isAimed;
    private float _attackTimer;
    private float _attackCooldown = 2f;
    #endregion

    #region Inherited Functions
    public override void StateStart(EnemyStateManager enemy)
    {
        _enemy = enemy;
    }
    public override void StateUpdate()
    {
        Aim();
        Attack();

        StateHandler();
    }
    public override void OnCollisionEnter(Collision collision)
    {

    }
    #endregion

    #region Behaviour Functions
    private void Aim()
    {
        Vector3 lookPos = _enemy.Target.position - _enemy.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, rotation, Time.deltaTime * 5f);

        if (Vector3.Angle(_enemy.transform.forward, lookPos) < 2f)
        {
            _attackTimer += Time.deltaTime;
            _isAimed = true;
        }
        else
        {
            _isAimed = false;
        }
    }
    private void Attack()
    {
        if (!_isAimed || _attackTimer < _attackCooldown) return;

        Debug.Log("Shoot: " + _attackTimer + "s");
        GameObject _projectile = GameObject.Instantiate(_enemy.ProjectilePrefab, _enemy.transform.position + _enemy.transform.forward, Quaternion.identity);
        _projectile.transform.rotation = _enemy.transform.rotation;
        _attackTimer = 0f;
    }
    #endregion

    #region State Functions
    private void StateHandler()
    {
        if (_enemy.Target != null)
        {
            bool inRange = Vector3.Distance(_enemy.transform.position, _enemy.Target.position) <= _enemy.ShootDistance;
            if (!inRange)
            {
                _enemy.SetState(_enemy.ChaseState);
            }
        }
    }
    #endregion
}
