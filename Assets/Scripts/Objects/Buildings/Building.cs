using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private GameSystem _gameSystem;

    [SerializeField] private SpriteRenderer _baseSprite;
    [SerializeField] private SpriteRenderer _turretSprite;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Projectile _projectile;

    private float _radius;
    private float _fireRate;

    private bool _isSelectedEnemy;
    private Enemy _selectedEnemy;

    public int Level { get; private set; }
    public int Price => _data.Data[Level].Price;
    public int UpgradePrice => _data.Data[Mathf.Min(_data.MaxLevel - 1, Level + 1)].Price;
    private BuildingsData _data;
    
    public BuildingType Type { get; private set; }

    public void Initialize(GameSystem gameSystem, BuildingsData data)
    {
        _gameSystem = gameSystem;
        _data = data;

        Type = data.Type;

        Setup(_data.Data[Level]);

        StartCoroutine(nameof(ShootEnemy));
    }

    public void Upgrade()
    {
        ++Level;
        Setup(_data.Data[Level]);
    }

    private void Setup(BuildingsData.BuildingLevel data)
    {
        _turretSprite.sprite = data.Icon;
        _radius = data.Radius;
        _fireRate = data.FireRate;
    }

    public void Update()
    {
       
    }

    public IEnumerator ShootEnemy()
    {
        while (true)
        {

            _isSelectedEnemy = false;
            float maxDistanceTraveled = 0;
            foreach (Enemy enemy in _gameSystem.EnemyController.ListOfEnemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= _radius && maxDistanceTraveled <= enemy.DistanceTraveled)
                {
                    _selectedEnemy = enemy;
                    maxDistanceTraveled = enemy.DistanceTraveled;
                    _isSelectedEnemy = true;
                }
            }

            if (_isSelectedEnemy)
            {
                _turretSprite.transform.rotation = Quaternion.Euler(0, 0, -90 + LookAtZAxis(_selectedEnemy.transform.position - transform.position));

                SpawnProjectile();

                Debug.DrawLine(transform.position, _selectedEnemy.transform.position, Color.white, 0.1f);
            }

            yield return new WaitForSeconds(_fireRate);
        }
    }

    private void SpawnProjectile()
    {
        Projectile projectile = GameObject.Instantiate(_projectile);
        projectile.Initialize(_gameSystem);
        projectile.transform.position = _bulletSpawnPoint.position;
        projectile.EnemyTarget = _selectedEnemy;

        _selectedEnemy.ListOfProjectiles.Add(projectile);
    }

    private float LookAtZAxis(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
