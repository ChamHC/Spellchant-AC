using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyState : EnemyState
{
    #region Variables
    private EnemyStateManager _enemy;
    private LineRenderer _patrolLineRenderer;
    private LineRenderer _viewLineRenderer;
    private float _patrolRadius;
    private bool _isCoroutineRunning;
    #endregion

    #region Inherited Functions
    public override void StateStart(EnemyStateManager enemy)
    {
        _enemy = enemy;
        _patrolRadius = _enemy.NavMeshAgent.stoppingDistance * 3f;
        SetRandomDestination();
        InitializeLineRenderer();
    }

    public override void StateUpdate()
    {
        DetectPlayer();

        if (_enemy.NavMeshAgent.remainingDistance <= _enemy.NavMeshAgent.stoppingDistance)
        {
            SetRandomDestination();
        }
        UpdateLineRenderer();
    }

    public override void OnCollisionEnter(Collision collision)
    {

    }
    #endregion

    private IEnumerator DelayedSetRandomDestination(Vector3 position)
    {
        _isCoroutineRunning = true;
        yield return new WaitForSeconds(2f);
        _enemy.NavMeshAgent.SetDestination(position);
        _isCoroutineRunning = false;
    }

    private void SetRandomDestination()
    {
        Vector3 randomPosition = GetRandomEdgePosition();
        if (randomPosition != Vector3.zero)
        {
            if (!_isCoroutineRunning)
            {
                _enemy.StartCoroutine(DelayedSetRandomDestination(randomPosition));
                Debug.Log(_enemy.gameObject.name + ": Patrolling...");
            }
        }
        else
        {
            _enemy.SetState(_enemy.PatrolState);
        }
    }

    private Vector3 GetRandomEdgePosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized * _patrolRadius;
        randomDirection += _enemy.transform.position;
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, _patrolRadius, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }

        Vector3 randomEdgePosition = GetRandomPositionWithinRadius();
        if (randomEdgePosition != Vector3.zero)
        {
            return randomEdgePosition;
        }

        return Vector3.zero;
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _patrolRadius;
        randomDirection += _enemy.transform.position;
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, _patrolRadius, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }

        return Vector3.zero;
    }

    private void InitializeLineRenderer()
    {
        _viewLineRenderer = _enemy.gameObject.GetComponentInChildren<LineRenderer>();
        _viewLineRenderer.startWidth = 0.1f;
        _viewLineRenderer.endWidth = 0.1f;
        _viewLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _viewLineRenderer.startColor = Color.red;
        _viewLineRenderer.endColor = Color.red;
        _viewLineRenderer.positionCount = 360;
        _viewLineRenderer.useWorldSpace = false;
    }

    private void UpdateLineRenderer()
    {
        float stoppingDistance = _enemy.NavMeshAgent.stoppingDistance;
        Vector3[] _positions = new Vector3[360];
        for (int i = 0; i < 360; i++)
        {
            float angle = i * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle) * stoppingDistance;
            float z = Mathf.Cos(angle) * stoppingDistance;
            _positions[i] = new Vector3(x, 0f, z);
        }
        _viewLineRenderer.SetPositions(_positions);
    }

    private void DetectPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        float viewDistance = _enemy.NavMeshAgent.stoppingDistance;
        float fov = 60f;

        float distanceToPlayer = Vector3.Distance(_enemy.transform.position, player.transform.position);
        if (distanceToPlayer <= viewDistance)   // Player is within view distance
        {
            Vector3 directionToPlayer = player.transform.position - _enemy.transform.position;
            float angleToPlayer = Vector3.Angle(_enemy.transform.forward, directionToPlayer);
            if (angleToPlayer <= fov / 2f)  // Player is within the FOV
            {
                RaycastHit hit;
                if (Physics.Raycast(_enemy.transform.position, directionToPlayer, out hit, viewDistance) && hit.collider.CompareTag("Player"))  // Player is within line of sight and no obstacles in between
                {
                    _enemy.SetState(_enemy.ChaseState);
                }
            }
        }
    }
}
