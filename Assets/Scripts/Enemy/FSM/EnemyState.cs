using UnityEngine;

public abstract class EnemyState
{
    public abstract void StateStart(EnemyStateManager _enemy);

    public abstract void StateUpdate();

    public abstract void OnCollisionEnter(Collision collision);
}
