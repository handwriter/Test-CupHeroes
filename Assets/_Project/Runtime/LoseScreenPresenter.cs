using System;
using UnityEngine;

namespace _Project.Runtime
{
    public class LoseScreenPresenter : MonoBehaviour
    {
        public Action OnRestartBtn; 
        [SerializeField] private LoseScreenView _view;

        public void Initialize()
        {
            _view.RestartBtn.onClick.AddListener(() => OnRestartBtn?.Invoke());
        }
        
        public void SetState(bool state) => _view.SetState(state);
    }
}