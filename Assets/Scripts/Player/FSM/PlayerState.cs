using UnityEngine;

public abstract class PlayerState
{
    public abstract void StateStart(PlayerStateManager _player);

    public abstract void StateUpdate();

    public abstract void OnCollisionEnter(Collision collision);
}
