using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Interaction.Flags.UI
{
    public class FlagPassiveView : MonoBehaviour
    {
        [SerializeField] private GameObject _bar;
        [SerializeField] private Image _fill;
        [SerializeField] private TextMeshProUGUI _capturedTime;

        private void Start()
        {
            _fill.fillAmount = 0f;
            DisableBar();
        }

        public void SetCapturedTime(float time) => _capturedTime.text = time.ToString(CultureInfo.InvariantCulture);

        public void SetValue(float current, float max) =>
            _fill.fillAmount = current / max;

        public void SetMaxValue() => _fill.fillAmount = 1f;

        public void DisableBar() => _bar.SetActive(false);
        public void EnableBar() => _bar.SetActive(true);
    }
}