using System;
using UniRx;
using UnityEngine;

namespace _Project.Runtime
{
    public class BonusCardsScreenPresenter : MonoBehaviour
    {
        public Action OnContinueBtn;
        [SerializeField] private BonusCardsScreenView _view;
        
        private BonusCardsScreenUseCase _useCase;
        
        public void Initialize(SaveLoadManager saveLoadManager, UpgradesConfig upgradesConfig)
        {
            _useCase = new BonusCardsScreenUseCase(saveLoadManager, upgradesConfig, _view);
            _useCase.Initialize();
            
            _view.ContinueBtn.onClick.AddListener(() => OnContinueBtn?.Invoke());
        }
        
        public void SetState(bool value) => _view.SetState(value);
    }
}