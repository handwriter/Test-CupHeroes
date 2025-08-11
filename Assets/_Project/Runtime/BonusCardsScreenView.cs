using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime
{
    public class BonusCardsScreenView : MonoBehaviour
    {
        public BonusCardView AttackCard;
        public BonusCardView TimeCard;
        public BonusCardView HpCard;
        public Button ContinueBtn;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetState(bool state)
        {
            _canvasGroup.alpha = state ? 1 : 0;
            _canvasGroup.interactable = state;
            _canvasGroup.blocksRaycasts = state;
        }
    }
}