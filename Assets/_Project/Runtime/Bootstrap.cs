using System;
using UnityEngine;
using VContainer;

namespace _Project.Runtime
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameFieldPresenter _gameField;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _characterSpawnPoint;
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private GameLoopController _gameLoopController;
        [SerializeField] private EnemySpawnPointData[] _enemySpawnPoints;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GunsConfig _gunsConfig;
        [SerializeField] private LevelUIRoot _levelUIRoot;
        [Inject] private SaveLoadManager _saveLoadManager;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _levelUIRoot.Bind(_gameConfig.UpgradesConfig, _gameLoopController, _gameConfig);
            
            var gunFactory = new GunFactory(_gunsConfig);
            
            var enemiesManager = new EnemiesManager(_enemySpawnPoints, _enemyPrefab,
                _characterRoot, _gameConfig, gunFactory, _saveLoadManager);
            
            _gameField.Initialize();
            
            var player = Instantiate(_player, _characterRoot);
            player.transform.position = _characterSpawnPoint.position;
            player.Initialize(_gameConfig.PlayerStartHealth, _saveLoadManager, _gameConfig);

            var playerGun = gunFactory.GetGun(player.StartGun);
            player.SetGun(playerGun);
            
            _gameLoopController.StartGame(player, _gameField, enemiesManager, _gameConfig);
        }
    }
}