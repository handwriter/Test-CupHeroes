using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Runtime
{
    class LevelUIRoot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private BonusCardsScreenPresenter _bonusCardsScreen;
        [SerializeField] private LoseScreenPresenter _loseScreen;
        [SerializeField] private TMP_Text _attackValueText;
        [SerializeField] private TMP_Text _attackSpeedText;
        [Inject] private SaveLoadManager _saveLoadManager;
        private const string _attackValueFormat = "Значение атаки: {0}"; 
        private const string _attackSpeedFormat = "Скорость атаки: {0}"; 
        
        public void Bind(UpgradesConfig upgradesConfig, GameLoopController gameLoopController, GameConfig gameConfig)
        {
            _saveLoadManager.Money.Subscribe(value => _moneyText.text = value.ToString());
            _bonusCardsScreen.Initialize(_saveLoadManager, upgradesConfig);

            gameLoopController.OnPauseGame += () => _bonusCardsScreen.SetState(true);
            _bonusCardsScreen.OnContinueBtn += () =>
            {
                _bonusCardsScreen.SetState(false);
                gameLoopController.StartWave();
            };
            
            _loseScreen.Initialize();
            gameLoopController.OnGameEnded += () => _loseScreen.SetState(true);
            _loseScreen.OnRestartBtn += () => gameLoopController.RestartGame();

            _saveLoadManager.UnlockedAttackUpgrade
                .TakeUntilDestroy(this)
                .Subscribe(value =>
                {
                    int attackValue = gameConfig.PlayerDamage;
                    if (value > 0)
                    {
                        var data = upgradesConfig.Attack[Mathf.Min(value - 1, upgradesConfig.Attack.Length - 1)];
                        attackValue += data.Value;
                    }

                    _attackValueText.text = string.Format(_attackValueFormat, attackValue);
                });
            
            _saveLoadManager.UnlockedTimeUpgrade
                .TakeUntilDestroy(this)
                .Subscribe(value =>
                {
                    int attackSpeed = 1;
                    if (value > 0)
                    {
                        var data = upgradesConfig.Time[Mathf.Min(value - 1, upgradesConfig.Time.Length - 1)];
                        attackSpeed = data.Value + 1;
                    }

                    _attackSpeedText.text = string.Format(_attackSpeedFormat, attackSpeed);
                });
        }
    }
}