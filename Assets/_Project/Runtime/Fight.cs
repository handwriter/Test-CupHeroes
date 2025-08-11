using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Runtime
{
    public class Fight : Object
    {
        public ReactiveProperty<int> AliveTeams = new ();
        private float _attacksDelay;
        private Team[] _teams;
        private CancellationTokenSource _cancellationTokenSource;
        private int _playerAdditionalAttacks = 0;
        private GameConfig _gameConfig;
        
        public Fight(float attacksDelay, SaveLoadManager saveLoadManager, GameConfig gameConfig)
        {
            _attacksDelay = attacksDelay;
            AliveTeams
                .Where(value => value == 1)
                .Subscribe(_ =>
                {
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource?.Dispose();
                });

            saveLoadManager.UnlockedTimeUpgrade
                .Subscribe((value) =>
                {
                    if (value > 0)
                    {
                        var data = gameConfig.UpgradesConfig.Time[
                            Mathf.Min(gameConfig.UpgradesConfig.Time.Length - 1, value - 1)];
                        _playerAdditionalAttacks = data.Value;
                    }
                });
            _gameConfig = gameConfig;
        }

        private async UniTask Attack(CancellationToken cancellationToken)
        {
            for (var i = 0; i < _playerAdditionalAttacks + 1; i++)
            {
                _teams[0].Attack(_teams[1].GetTargetPosition());
                await UniTask.Delay(TimeSpan.FromSeconds(_gameConfig.PlayerAttacksDelay),
                    cancellationToken: cancellationToken);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_attacksDelay), cancellationToken: cancellationToken);
            if (AliveTeams.Value > 1)
                _teams[1].Attack(_teams[0].GetTargetPosition());
            await UniTask.Delay(TimeSpan.FromSeconds(_attacksDelay), cancellationToken: cancellationToken);
            if (AliveTeams.Value > 1)
                await Attack(cancellationToken);
        }
        
        public void StartFighting(Team[] teams)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;
            AliveTeams.Value = teams.Length;

            _teams = teams;

            Attack(cancellationToken).Forget();
        }
    }
}