using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime
{
    public class BonusCardView : MonoBehaviour
    {
        public TMP_Text ValueText;
        public Button BuyBtn;
        public TMP_Text BuyBtnText;
        public TMP_Text AllBougthTxt;

        public void SetData(UpgradeData upgradeData, bool active, bool isAllBougth)
        {
            ValueText.text = "+" + upgradeData.Value;
            BuyBtnText.text = upgradeData.Cost.ToString();
            
            BuyBtn.interactable = active;
            BuyBtnText.gameObject.SetActive(!isAllBougth);
            AllBougthTxt.gameObject.SetActive(isAllBougth);
        }
    }
}