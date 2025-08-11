using UniRx;
using UnityEngine;

namespace _Project.Runtime
{
    public class GameFieldPresenter : MonoBehaviour
    {
        [SerializeField] private GameFieldView _view;
        private GameFieldUseCase _useCase;

        public void Initialize()
        {
            _useCase = new GameFieldUseCase();
            _useCase.Speed.Subscribe((speed) => _view.SetSpeed(speed));
        }

        public void SetMoveState(bool value) => _useCase.SetState(value);
    }
}