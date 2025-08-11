using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using Random = UnityEngine.Random;

namespace _Project.Runtime
{
    public class GameLoopController : MonoBehaviour
    {
        public Action OnPauseGame;
        public Action OnGameEnded;
        
        [Inject] private SaveLoadManager _saveLoadManager;
        private Player _player;
        private GameFieldPresenter _gameField;
        private EnemiesManager _enemiesManager;
        private GameConfig _gameConfig;
        private ReactiveProperty<int> _enemiesCount = new();
        private int _targetCount;
        private readonly Team _enemiesTeam = new ();
        private readonly Team _playerTeam = new ();
        private Fight _fight;

        public void StartWave()
        {
            _enemiesCount.Value = 0;
            _player.SetWalkState(true);
            _gameField.SetMoveState(true);
            
            _targetCount = Random.Range(1, _gameConfig.MaxEnemiesCount + 1);
            var enemies = _enemiesManager.GetEnemies(_targetCount);
            foreach (var enemy in enemies)
                enemy.IsReady
                    .Where((value) => value)
                    .Subscribe((_) => _enemiesCount.Value++);
            _enemiesTeam.SetupMembers(enemies.Select((enemy) => (ITeamMember)enemy).ToArray());
        }

        private void StartFight()
        {
            _enemiesCount.Value = 0;
            _player.SetWalkState(false);
            _gameField.SetMoveState(false);
            
            _fight.StartFighting(new [] {_playerTeam, _enemiesTeam});
        }

        public void RestartGame()
        {
            _saveLoadManager.Money.Value = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void StartGame(Player player, GameFieldPresenter gameField, EnemiesManager enemiesManager, GameConfig gameConfig)
        {
            _player = player;
            _gameField = gameField;
            _enemiesManager = enemiesManager;
            _gameConfig = gameConfig;
            _fight = new Fight(gameConfig.DelayBetweenTeamAttacks, _saveLoadManager, gameConfig);
            
            _playerTeam.SetupMembers(new ITeamMember[] { player });
            
            _playerTeam.AliveMembers
                .Where(value => value == 0)
                .Subscribe(_ => _fight.AliveTeams.Value--);
            _enemiesTeam.AliveMembers
                .Where(value => value == 0)
                .Subscribe(_ => _fight.AliveTeams.Value--);
            _enemiesCount
                .Where((count) => count == _targetCount && _targetCount != 0)
                .Subscribe((_) => StartFight());
            
            _fight.AliveTeams
                .Where((value) => value == 1)
                .Subscribe((_) =>
                {
                    if (_playerTeam.AliveMembers.Value != 0)
                        OnPauseGame?.Invoke();
                    else
                        OnGameEnded?.Invoke();
                });
            
            StartWave();
        }
    }
}