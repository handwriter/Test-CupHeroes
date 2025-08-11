using UniRx;
using UnityEngine;

public class GameFieldView : MonoBehaviour
{
    [SerializeField] private Animator _skyAnimator;
    [SerializeField] private Animator _roadAnimator;
    [SerializeField] private Animator _groundAnimator;
    private static readonly int _speedParameter = Animator.StringToHash("Speed");
    
    public void SetSpeed(float value)
    {
        _skyAnimator.SetFloat(_speedParameter, value);
        _roadAnimator.SetFloat(_speedParameter, value);
        _groundAnimator.SetFloat(_speedParameter, value);
    }
}
