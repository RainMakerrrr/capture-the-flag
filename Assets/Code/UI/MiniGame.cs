using System.Collections;
using Code.Interaction.Flags.Mediator;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class MiniGame : MonoBehaviour, IMediatorReceiver
    {
        [SerializeField] private Image _gameHolder;
        [SerializeField] private RectTransform _winArea;
        [SerializeField] private RectTransform _slider;
        [SerializeField] private Button _areaButton;

        public string Id => GetInstanceID().ToString();

        private IMediator _mediator;
        private Coroutine _coroutine;

        private string _id;
        private string _flagId;
        private float _lifeTime;
        private float _winAreaSize;

        public void Construct(IMediator mediator, float lifeTime, float winAreaSize)
        {
            _mediator = mediator;
            _lifeTime = lifeTime;
            _winAreaSize = winAreaSize;
        }
        
        public void InitAreaSize() => _winArea.sizeDelta = new Vector2(_winAreaSize, _winArea.sizeDelta.y);

        public void Notify(MessageType messageType, string id)
        {
            if (messageType == MessageType.Failure) return;

            _flagId = id;
            _gameHolder.gameObject.SetActive(true);
            _areaButton.onClick.AddListener(OnAreaClicked);
            _coroutine = StartCoroutine(DisableAfterDelay());
        }

        private void OnAreaClicked()
        {
            _mediator.Send(GetMiniGameResultMessage(), this, _flagId);

            StopCoroutine(_coroutine);

            _areaButton.onClick.RemoveListener(OnAreaClicked);
            _gameHolder.gameObject.SetActive(false);
        }

        private MessageType GetMiniGameResultMessage() => _winArea.rect.Contains(_slider.anchoredPosition)
            ? MessageType.Success
            : MessageType.Failure;


        private IEnumerator DisableAfterDelay()
        {
            yield return new WaitForSeconds(_lifeTime);

            _mediator.Send(MessageType.Failure, this, _flagId);

            _gameHolder.gameObject.SetActive(false);
        }
    }
}