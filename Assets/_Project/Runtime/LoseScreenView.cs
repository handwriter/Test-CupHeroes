using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime
{
    public class LoseScreenView : MonoBehaviour
    {
        public Button RestartBtn;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetState(bool state)
        {
            _canvasGroup.alpha = state ? 1 : 0;
            _canvasGroup.interactable = state;
            _canvasGroup.blocksRaycasts = state;
        }
    }
}