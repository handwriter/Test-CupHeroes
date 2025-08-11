using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Runtime
{
    public class EnemiesManager
    {
        private EnemySpawnPointData[] _spawnPoints;
        private GameConfig _gameConfig;
        private GunFactory _gunFactory;
        private GameObject _enemyPrefab;
        private Transform _spawnRoot;
        private SaveLoadManager _saveLoadManager;
        
        public EnemiesManager(EnemySpawnPointData[] spawnPoints, GameObject enemyPrefab, Transform spawnRoot, GameConfig gameConfig, GunFactory gunFactory, SaveLoadManager saveLoadManager)
        {
            _spawnPoints = spawnPoints;
            _gameConfig = gameConfig;
            _gunFactory = gunFactory;
            _enemyPrefab = enemyPrefab;
            _spawnRoot = spawnRoot;
            _saveLoadManager = saveLoadManager;
        }

        private Enemy CreateEnemy(GameObject enemyPrefab, Transform spawnRoot)
        {
            var enemy = Object.Instantiate(enemyPrefab, spawnRoot).GetComponent<Enemy>();
            enemy.Initialize(_gameConfig.EnemyHealth);

            enemy.Health
                .Where(value => value == 0)
                .Subscribe((_) =>
                {
                    _saveLoadManager.Money.Value += _gameConfig.RewardForEnemies;
                    Object.Destroy(enemy.gameObject);
                });
            
            var enemyGun = _gunFactory.GetGun(enemy.StartGun);
            enemyGun.GetComponent<IGun>().Damage = _gameConfig.EnemyDamage;
            enemy.SetGun(enemyGun);
            return enemy;
        }
        
        public Enemy[] GetEnemies(int count)
        {
            List<Enemy> enemies = new List<Enemy>();
            var positions = Utils.Shuffle(_spawnPoints);
            for (int i = 0; i < count; i++)
            {
                var enemy = CreateEnemy(_enemyPrefab, _spawnRoot);
                enemy.Setup(positions[i]);
                enemies.Add(enemy);
            }
            return enemies.ToArray();
        }
    }
}