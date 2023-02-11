using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Player.UI
{
    public class CaptureBanSlider : MonoBehaviour
    {
        [SerializeField] private Image _slider;
        [SerializeField] private Image _backGround;

        private IFlagInvader _flagInvader;

        public void Construct(IFlagInvader flagInvader)
        {
            _flagInvader = flagInvader;
        }

        public void Enable()
        {
            _backGround.gameObject.SetActive(false);
            _flagInvader.Banned += OnBanned;
        }

        private void OnBanned(float banTime)
        {
            _backGround.gameObject.SetActive(true);

            _slider.fillAmount = 1f;
            _slider.DOFillAmount(0f, banTime).SetEase(Ease.Linear)
                .OnComplete(() => _backGround.gameObject.SetActive(false));
        }

        private void OnDestroy()
        {
            _flagInvader.Banned -= OnBanned;
        }
    }
}