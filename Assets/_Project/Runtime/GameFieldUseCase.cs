using UniRx;

namespace _Project.Runtime
{
    public class GameFieldUseCase
    {
        public ReactiveProperty<float> Speed = new ();

        public void SetState(bool state)
        {
            Speed.Value = state ? 1 : 0;
        }
    }
}