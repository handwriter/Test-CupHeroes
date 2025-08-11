using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthTxt;
        [SerializeField] private TMP_Text _damageTxt;
        [SerializeField] private Image _fill;
        private int _value;
        
        public void SetData(float percent, int value)
        {
            if (value < _value)
            {
                _damageTxt.text = (_value - value).ToString();
                _damageTxt.gameObject.SetActive(true);
                _damageTxt.GetComponent<Animator>().Rebind();
            }
            _healthTxt.text = value.ToString();
            _fill.fillAmount = percent;
            _value = value;
        }
    }
}