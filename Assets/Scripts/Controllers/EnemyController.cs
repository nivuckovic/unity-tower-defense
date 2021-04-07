using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Tank,
    Soldier
}

public class EnemyController : BaseController
{
    public List<Enemy> ListOfEnemies = new List<Enemy>();

    public EnemyController(GameSystem gameSystem) : base(gameSystem)
    {
    }

    public void DestroyEnemy(Enemy enemy)
    {
        ListOfEnemies.Remove(enemy);

        _gameSystem.GameState.Give(enemy.Reward);
        _gameSystem.ParticlesController.PlayParticle(ParticlesController.ParticleType.Cash, enemy.transform.position);

        enemy.Destroy();
    }
}
