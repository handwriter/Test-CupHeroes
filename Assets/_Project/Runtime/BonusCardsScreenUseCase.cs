using System;
using UniRx;
using UnityEngine;

namespace _Project.Runtime
{
    public class BonusCardsScreenUseCase
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly UpgradesConfig _upgradesConfig;
        private readonly BonusCardsScreenView _view;
        
        public BonusCardsScreenUseCase(SaveLoadManager saveLoadManager, UpgradesConfig upgradesConfig, BonusCardsScreenView view)
        {
            _saveLoadManager = saveLoadManager;
            _upgradesConfig = upgradesConfig;
            _view = view;
        }
        
        public void Initialize()
        {
            SetupAttackCard();
            SetupHpCard();
            SetupTimeCard();
        }
        
        private void SetupAttackCard()
        {
            _saveLoadManager.UnlockedAttackUpgrade
                .TakeUntilDestroy(_view)
                .Subscribe(value =>
                {
                    var data = _upgradesConfig.Attack[Mathf.Min(value, _upgradesConfig.Attack.Length - 1)];
                    bool isAllBougth = _saveLoadManager.UnlockedAttackUpgrade.Value == _upgradesConfig.Attack.Length;
                    bool active = _saveLoadManager.Money.Value >= data.Cost && !isAllBougth;
                    _view.AttackCard.SetData(data, active, isAllBougth);
                });
                
            _saveLoadManager.Money
                .TakeUntilDestroy(_view)
                .Subscribe(value =>
                {
                    var data = _upgradesConfig.Attack[Mathf.Min(_upgradesConfig.Attack.Length - 1, _saveLoadManager.UnlockedAttackUpgrade.Value)];
                    bool isAllBougth = _saveLoadManager.UnlockedAttackUpgrade.Value == _upgradesConfig.Attack.Length;
                    bool active = value >= data.Cost && !isAllBougth;
                    _view.AttackCard.SetData(data, active, isAllBougth);
                });
                
            _view.AttackCard.BuyBtn.onClick.AddListener(() =>
            {
                var data = _upgradesConfig.Attack[Mathf.Min(_upgradesConfig.Attack.Length - 1, _saveLoadManager.UnlockedAttackUpgrade.Value)];
                _saveLoadManager.Money.Value -= data.Cost;
                _saveLoadManager.UnlockedAttackUpgrade.Value++;
            });
        }
        
        private void SetupHpCard()
        {
            _saveLoadManager.UnlockedHpUpgrade
                .TakeUntilDestroy(_view)
                .Subscribe(value =>
                {
                    var data = _upgradesConfig.HP[Mathf.Min(_upgradesConfig.HP.Length - 1, value)];
                    bool isAllBougth = _saveLoadManager.UnlockedHpUpgrade.Value == _upgradesConfig.HP.Length;
                    bool active = _saveLoadManager.Money.Value >= data.Cost && !isAllBougth;
                    _view.HpCard.SetData(data, active, isAllBougth);
                });
                
            _saveLoadManager.Money
                .TakeUntilDestroy(_view)
                .Subscribe(value =>
                {
                    var data = _upgradesConfig.HP[Mathf.Min(_upgradesConfig.HP.Length - 1, _saveLoadManager.UnlockedHpUpgrade.Value)];
                    bool isAllBougth = _saveLoadManager.UnlockedHpUpgrade.Value == _upgradesConfig.HP.Length;
                    bool active = value >= data.Cost && !isAllBougth;
                    _view.HpCard.SetData(data, active, isAllBougth);
                });
                
            _view.HpCard.BuyBtn.onClick.AddListener(() =>
            {
                var data = _upgradesConfig.HP[Mathf.Min(_upgradesConfig.HP.Length - 1, _saveLoadManager.UnlockedHpUpgrade.Value)];
                _saveLoadManager.Money.Value -= data.Cost;
                _saveLoadManager.UnlockedHpUpgrade.Value++;
            });
        }
        
        private void SetupTimeCard()
        {
            _saveLoadManager.UnlockedTimeUpgrade
                .TakeUntilDestroy(_view)
                .Subscribe(value =>
                {
                    var data = _upgradesConfig.Time[Mathf.Min(_upgradesConfig.Time.Length - 1, value)];
                    bool isAllBougth = _saveLoadManager.UnlockedTimeUpgrade.Value == _upgradesConfig.Time.Length;
                    bool active = _saveLoadManager.Money.Value >= data.Cost && !isAllBougth;
                    _view.TimeCard.SetData(data, active, isAllBougth);
                });
                
            _saveLoadManager.Money
                .TakeUntilDestroy(_view)
                .Subscribe(value =>
                {
                    var data = _upgradesConfig.Time[Mathf.Min(_upgradesConfig.Time.Length - 1, _saveLoadManager.UnlockedTimeUpgrade.Value)];
                    bool isAllBougth = _saveLoadManager.UnlockedTimeUpgrade.Value == _upgradesConfig.Time.Length;
                    bool active = value >= data.Cost && !isAllBougth;
                    _view.TimeCard.SetData(data, active, isAllBougth);
                });
                
            _view.TimeCard.BuyBtn.onClick.AddListener(() =>
            {
                var data = _upgradesConfig.Time[Mathf.Min(_upgradesConfig.Time.Length - 1, _saveLoadManager.UnlockedTimeUpgrade.Value)];
                _saveLoadManager.Money.Value -= data.Cost;
                _saveLoadManager.UnlockedTimeUpgrade.Value++;
            });
        }
    }
} 