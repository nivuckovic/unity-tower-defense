using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy _enemyTarget;
    public Enemy EnemyTarget
    {
        get => _enemyTarget;
        set => _enemyTarget = value;
    }
    public float Speed;
    public int Damage;

    private Vector3 _targetPosition;

    GameSystem _gameSystem;

    public void Initialize(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;
    }

    public void Update()
    {
        bool targetDestroyed = true;
        if (_enemyTarget != null)
        {
            _targetPosition = _enemyTarget.transform.position;
            targetDestroyed = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _targetPosition) < 0.001f)
        {
            if (!targetDestroyed)
                _enemyTarget.DamageEnemy(Damage);

            Destroy(this.gameObject);
        }
    }

    public void OnTargetDestroyed()
    {
        _enemyTarget = null;
    }
}
