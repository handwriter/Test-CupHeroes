using UnityEngine;

namespace _Project.Runtime
{
    public class HealthBarPresenter : MonoBehaviour
    {
        [SerializeField] private HealthBarView _view;
        
        public void SetData(float percent, int value) => _view.SetData(percent, value);
    }
}