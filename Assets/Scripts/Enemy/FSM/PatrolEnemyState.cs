using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyState : EnemyState
{
    #region Variables
    private EnemyStateManager _enemy;
    #endregion

    #region Inherited Functions
    public override void StateStart(EnemyStateManager enemy)
    {
        _enemy = enemy;
    }
    public override void StateUpdate()
    {

    }
    public override void OnCollisionEnter(Collision collision)
    {

    }
    #endregion
}
