using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyType EnemyType;
    public SpriteRenderer SpriteRenderer;
    public int Health;
    public float Speed;
    public int Damage;
    public int Reward;

    private PathCreator _pathCreator;
    private float _distanceTraveled;
    public float DistanceTraveled => _distanceTraveled;

    public List<Projectile> ListOfProjectiles = new List<Projectile>();

    private GameSystem _gameSystem;

    [SerializeField] HealthBar _healthBar;

    public void Initialize(GameSystem gameSystem, EnemyData data, PathCreator path)
    {
        _gameSystem = gameSystem;

        EnemyType = data.EnemyType;
        SpriteRenderer.sprite = data.Image;
        Health = data.Health;
        Speed = data.Speed;
        Damage = data.Damage;
        Reward = data.Reward;

        _pathCreator = path;

        _healthBar.SetMaxHealth(Health);
    }

    public void Update()
    {
        _distanceTraveled += Speed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);

        float angle = _pathCreator.path.GetRotationAtDistance(_distanceTraveled, EndOfPathInstruction.Stop).eulerAngles.x;
        SpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -angle);

        if (transform.position == _pathCreator.path.GetPoint(_pathCreator.path.NumPoints - 1))
        {
            if(--_gameSystem.GameState.Lives == 0)
            {
                #if UNITY_EDITOR
                    // Application.Quit() does not work in the editor so
                    // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

            }

            Destroy();
        }
    }

    public void OnDestroy()
    {
        foreach (Projectile projectile in ListOfProjectiles)
            projectile.OnTargetDestroyed();
    }

    public void DamageEnemy(int damage)
    {
        Health -= damage;
        _healthBar.SetHealth(Health);

        if(Health <= 0)
        {
            _gameSystem.EnemyController.DestroyEnemy(this);
        }
    }

    public void Destroy()
    {
        Object.Destroy(this.gameObject);
    }
}
